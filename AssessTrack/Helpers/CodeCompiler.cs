using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using AssessTrack.Models;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace AssessTrack.Helpers
{
    public static class CodeCompiler
    {
        public static void CompileCodeQuestions(this SubmissionRecord record)
        {
            string VSVarsPath = Environment.GetEnvironmentVariable("VS90COMNTOOLS");
            string ExeArgs = @"/c ""{0}vsvars32"" & cl {1} /EHsc";
            string failMsg = "\nCompile failed for Question #{0}, Answer #{1} on {2}.\n";
            string longrunMsg = "\nExecution failed for Question #{0}, Answer #{1} on {2}.\n Program did not run in allotted time span.\n";
            string successMessage = "\nCompile and Execute succeeded for Question #{0}, Answer #{1} on {2}.\nOutput: \n{3}\n----------------------\n";
            
            //Create working directory
            string tempDir = Environment.GetEnvironmentVariable("TEMP") + "\\cms-compiles\\" + record.SubmissionRecordID.ToString();
            Directory.CreateDirectory(tempDir);

            foreach (Response response in record.Responses)
            {
                if (response.Answer.Type == "code-answer")
                {
                    string source = response.ResponseID.ToString() + ".cpp";
                    string args = string.Format(ExeArgs, VSVarsPath, source,tempDir);
                    File.WriteAllText(tempDir + "\\" + source, response.ResponseText);
                    ProcessStartInfo startInfo = new ProcessStartInfo("cmd", args);
                    startInfo.WorkingDirectory = tempDir;
                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process compiler = new Process();
                    compiler.StartInfo = startInfo;
                    compiler.Start();
                    compiler.WaitForExit(5000); //Wait 5 seconds for compiler to finish
                    //Just to be safe, KILL it
                    //If it's not done yet then something dumb happened (probably)
                    if (!compiler.HasExited)
                        compiler.Kill();

                    string exe = tempDir + @"\" + response.ResponseID.ToString() + ".exe";
                    if (!File.Exists(exe))
                    {
                        record.Comments += string.Format(failMsg, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString());
                        continue;
                    }

                    //Run program
                    ProcessStartInfo userProgramInfo = new ProcessStartInfo(exe);
                    userProgramInfo.WorkingDirectory = tempDir;
                    userProgramInfo.UseShellExecute = false;
                    userProgramInfo.RedirectStandardOutput = true;
                    userProgramInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    userProgramInfo.CreateNoWindow = true;
                    Process userProgram = new Process();
                    userProgram.StartInfo = userProgramInfo;
                    userProgram.Start();
                    int seconds = 0;
                    while (seconds < 30 && !userProgram.HasExited)
                    {
                        Thread.Sleep(1000);
                        seconds++;
                    }
                    if (seconds == 30) //Program failed to run allotted time. KILL IT
                    {
                        userProgram.Kill();
                        record.Comments += string.Format(longrunMsg, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString());
                        continue;
                    }
                    //Get output and set comments
                    string output = userProgram.StandardOutput.ReadToEnd();
                    record.Comments += string.Format(successMessage, response.Answer.Question.Number, response.Answer.Number, DateTime.Now.ToString(),output);
                }
            }

            //Delete Temp Folder
            Directory.Delete(tempDir, true);
        }
    }
}
