using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;

namespace AI.Web.SOAPServiceLibrary.DomainObjects
{
    internal static class PythonRunner
    {
        internal static void RunPythonScript(string pythonLocation,
            string scriptPath,
            string arguments,
            out string errors,
            out string results)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = pythonLocation;
            psi.Arguments = $"\"{scriptPath}\"" + arguments;
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }
        }

        internal static void RunPythonScript(string pythonLocation,
            string scriptPath,
            out string errors,
            out string results)
        {
            var psi = new ProcessStartInfo();
            psi.FileName = pythonLocation;
            psi.Arguments = $"\"{scriptPath}\"";
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardError = true;

            using (var process = Process.Start(psi))
            {
                errors = process.StandardError.ReadToEnd();
                results = process.StandardOutput.ReadToEnd();
            }
        }

        internal static void RunPythonScript(ObjectStorePatternDominObject objectStorePatternDominObject , 
            string pythonLocation,
            Identifier ID,
            char delimiter,
            string scriptPath,
            out string errors,
            out string results)
        {
            var errorsI = "";
            var resultsI = "";
            foreach (var row in objectStorePatternDominObject.Getwhere(ID,delimiter))
            {
                var arguments = " ";
                foreach(var arg in arguments.Split(delimiter))
                {
                    arguments += arg + " ";
                }
                var psi = new ProcessStartInfo();
                psi.FileName = pythonLocation;
                psi.Arguments = $"\"{scriptPath}\"" + arguments;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                using (var process = Process.Start(psi))
                {
                    errorsI += process.StandardError.ReadToEnd();
                    resultsI += process.StandardOutput.ReadToEnd();
                }
            }
            errors = errorsI;
            results = resultsI;
        }

        internal static void RunPythonScript(ObjectByteStorePatternDominObject objectByteStorePatternDominObject, 
            string pythonLocation,
            Identifier ID,
            char delimiter,
            string scriptPath,
            string arguments,
            out string errors,
            out string results)
        {
            var errorsI = "";
            var resultsI = "";
            foreach (var row in objectByteStorePatternDominObject.Getwhere(ID))
            {
                var streamLocation = File.Create(ConfigurationManager.AppSettings["tempfile"]);
                streamLocation.Write(row, 0, row.Length);
                var psi = new ProcessStartInfo();
                psi.FileName = pythonLocation;
                psi.Arguments = $"\"{scriptPath}\"" + streamLocation;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                psi.RedirectStandardOutput = true;
                psi.RedirectStandardError = true;

                using (var process = Process.Start(psi))
                {
                    errorsI += process.StandardError.ReadToEnd();
                    resultsI += process.StandardOutput.ReadToEnd();
                }
            }
            errors = errorsI;
            results = resultsI;
        }

    }
}
