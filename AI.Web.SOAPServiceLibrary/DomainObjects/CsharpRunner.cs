using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;

namespace AI.Web.SOAPServiceLibrary.DomainObjects
{
    public static class CsharpRunner
    {
        private static IList<IList<double>> gridValues;
        private static IList<double> getResults;
        public static string compile(string cSharpCode, string[] libraryInclusionList)
        {
            //var testDbl = new List<IList<double>>() { new List<double>() {1,2,3,4,5,6 } };
            //var resTbl = ParseExpressionAgainstInMemmoryModel(testDbl, "u*v");
            string res = "";
            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });
            var parameters = new CompilerParameters(libraryInclusionList,"OutPut.exe",true);//new[] { "mscorlib.dll", "System.Core.dll" }, "YourApp.exe", true);
            parameters.GenerateExecutable = true;
            CompilerResults results = csc.CompileAssemblyFromSource(parameters, cSharpCode);
            res += results.Output + "|" + results.NativeCompilerReturnValue;
            results.Errors.Cast<CompilerError>().ToList().ForEach(error => res += error + "\n");// Console.WriteLine(error.ErrorText));
            return res;
            ;
        }


        public static IList<IList<double>> ParseExpressionAgainstInMemmoryModel(IList<IList<double>> GridValues, string expression)
        {
            gridValues = GridValues;
            //var conIns = new ConnectedInstruction();
            //var tempTest = (double) conIns.GetField(7, 4);
            #region HandleNullGrids
            if (gridValues == null || getResults ==  null)
            {
                getResults = new List<double>();
                ///
            }
            #endregion            

            var csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } });

            CompilerParameters cp;
            string code;
            ParseExpressionCodeOutputerAgainstLoadedInMemoryData(expression, out cp, out code);

            var results = csc.CompileAssemblyFromSource(cp, code);
            InvokeAndErrorhandle(results);

            return gridValues;
        }

        private static List<double> ActionFunc(IList<IList<double>> gridValues,
    Func<double, double, double,
    double, double, double, double> expression)
        {
            var result = new List<double>();
            foreach (var row in gridValues)
            {
                var calc = expression(row[0], row[1], row[2], row[3], row[4], row[5]);
                row.Add(calc);
                result.Add(calc);
                Console.WriteLine(calc.ToString());
            }
            return result;
        }

        private static void ParseExpressionCodeOutputerAgainstLoadedInMemoryData(string expressionUsingUVWXYZ, out CompilerParameters cp, out string code)
        {
            #region DynamicCodeInjectionForEfficientParsingOfScript
            cp = new CompilerParameters()
            {
                GenerateExecutable = false,
                OutputAssembly = "outputAssemblyName",
                GenerateInMemory = true
            };
            cp.ReferencedAssemblies.Add("mscorlib.dll");
            cp.ReferencedAssemblies.Add("System.dll");
            cp.ReferencedAssemblies.Add("AI.Web.SOAPServiceLibrary.dll");
            //The following seems to be unnecessary //cp.ReferencedAssemblies.Add(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location));//Assembly.GetEntryAssembly().Location);
            //StringBuilder sb = new StringBuilder();
            // This is Dangerous as it is open to code injection by design. Any implementation of this pattern
            // must be secured by the implementer

            // The string can contain any valid c# code and Rajah knows is best best stored in separate text class file
            code = @"

                using System;
                using System.Collections.Generic;
                namespace AI.Web.RealTimeCompile
                {
                    public class RuntimeClass
                    {
                        public static List<double> Main (IList<IList<double>> gridValues)
                        {
                            return ActionFunc(gridValues ,(u, v, w, x, y, z) => " + expressionUsingUVWXYZ + @"); 
                        }

                        private static List<double> ActionFunc(IList<IList<double>> gridValues,
                            Func<double, double, double,
                            double, double, double, double> expression)
                        {
                            var result = new List<double>();
                            foreach (var row in gridValues)
                            {
                                var calc = expression(row[0], row[1], row[2], row[3], row[4], row[5]);
                                row.Add(calc);
                                result.Add(calc);
                                Console.WriteLine(calc.ToString());
                            }
                            return result;
                        }
                    }
                }

                ";
            // "+ expression + @" 
            #endregion
        }
      
        private static void InvokeAndErrorhandle(CompilerResults results)
        {
            if (results.Errors.HasErrors)
            {
                string errors = "";
                foreach (CompilerError error in results.Errors)
                {
                    errors += string.Format("Error #{0}: {1}\n", error.ErrorNumber, error.ErrorText);
                }
                Console.Write(errors);
            }
            else
            {
                Assembly assembly = results.CompiledAssembly;
                Type program = assembly.GetType("AI.Web.RealTimeCompile.RuntimeClass");
                MethodInfo main = program.GetMethod("Main");
                object[] parameters = new object[1];
                parameters[0] = gridValues;
                List<double> returnType = new List<double>();
                main.Invoke(returnType, parameters);
                //Console.WriteLine("Square root = \u221A");
            }
        }


    }
}
