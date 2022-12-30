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
        public static string compile(string cSharpCode, string[] libraryInclusionList)
        {
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
    }
}
