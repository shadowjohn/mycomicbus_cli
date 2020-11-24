using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using utility;
using OdeToCode.Utility;
//using System.Runtime.Serialization.Json;
using System.IO;
using System.Web;

namespace mycomicbus_cli
{
    class Program
    {
        static string URL = "https://comicbus.live/online/a-18838.html?ch=1";
        static string MESSAGE = @"
Usage :
    mycomicbus_cli.exe ""URL""
    mycomicbus_cli.exe test
    mycomicbus_cli.exe """ + URL + @"""    
";
        static public void echo(string data)
        {
            Console.WriteLine(data);
        }
        static public void exit()
        {
            Environment.Exit(1);
        }
        //From : https://weblog.west-wind.com/posts/2007/feb/14/evaluating-javascript-code-from-c
        public static Microsoft.JScript.Vsa.VsaEngine Engine = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
        public static object EvalJScript(string JScript)
        {
            object Result = null;
            try
            {
                Result = Microsoft.JScript.Eval.JScriptEvaluate(JScript, Engine);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return Result;
        }
        static myinclude my = new myinclude();
        static void Main(string[] args)
        {
            if (args.Count() < 1)
            {
                echo(MESSAGE);
                exit();
            }
            if (my.is_string_like(args[0], "https://") || my.is_string_like(args[0], "http://"))
            {
                URL = args[0];
            }
            if (!my.is_string_like(URL, "https://") && !my.is_string_like(URL, "http://"))
            {
                echo(MESSAGE);
                exit();
            }

            string data = my.b2s(my.file_get_contents(URL));
            //echo(data);
            List<string> preScripts = new List<string>();
            preScripts.Add("var y='';");
            preScripts.Add("function spp(){};");
            preScripts.Add("var WWWWWTTTTTFFFFF='';");
            preScripts.Add("var document='';");
            preScripts.Add("function mm(p){return (parseInt((p-1)/10)%10)+(((p-1)%10)*3)};");
            preScripts.Add("function nn(n){return n<10?'00'+n:n<100?'0'+n:n;};");
            preScripts.Add("function su(a,b,c){var e=(a+'').substring(b,b+c);return (e);};");
            preScripts.Add("function lc(l){ if (l.length != 2) return l; var az = \"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\"; var a = l.substring(0, 1); var b = l.substring(1, 2); if (a == \"Z\") return 8000 + az.indexOf(b); else return az.indexOf(a) * 52 + az.indexOf(b); };");
            var mScripts = my.explode("<script>",data)[4];
            string scripts = my.explode("</script>", mScripts)[0].Trim();
            //echo(scripts);
            //exit();
            scripts = scripts.Replace("ge('TheImg').src=", "WWWWWTTTTTFFFFF=");
            //scripts = scripts.Replace(";", ";\n");
            //scripts = scripts.Replace("adsbygoogle", "//adsbygoogle");
            preScripts.Add(scripts);
            preScripts.Add("ps+\"___\"+\"https:\"+WWWWWTTTTTFFFFF;");
            //preScripts.Add("swtvv;");
            string sc = my.implode("\n", preScripts);
            //echo(sc);
            string finalData = EvalJScript(sc).ToString();
            //echo(sc);
            //exit();
            var m = my.explode("___", finalData);
            int pages = Convert.ToInt32(m[0]);
            string output = "Totals:" + pages.ToString()+"\n";
            string t = m[1].Replace("/001_", "/{PAGE}_");
            for (int i=1;i<=pages;i++)
            {
                //取得 WWWWWTTTTTFFFFF=...... 至 '.jpg';
                //WWWWWTTTTTFFFFF='//img'+su(yvdnl, 0, 1)+'.8comic.com/'+su(yvdnl,1,1)+'/'+ti+'/'+iyjco+'/'+ nn(p)+'_'+su(qvjme,mm(p),3)+'.jpg';
                //string d = t.Replace("{PAGE}", i.ToString().PadLeft(3, '0'));
                //output += d + "\n";
                var imgPath = "'https:"+my.get_between(sc,";WWWWWTTTTTFFFFF='", "';");
                var _sc = sc + imgPath.Replace("(p)", "(" + i + ")")+"';";
                output+= EvalJScript(_sc).ToString()+"\n";
                //echo(output);
                //exit();
            }
            echo(output);
        }
    }
}
