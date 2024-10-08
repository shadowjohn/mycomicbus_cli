﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using utility;

namespace mycomicbus_cli
{
    class Program
    {
        static string URL = "https://www.comicabc.com/online/new-18838.html?ch=1";
        static int THREAD_COUNT = 1; //預設執行緒數
        static string MESSAGE = @"
Usage :
    mycomicbus_cli.exe ""URL""
    mycomicbus_cli.exe test
    mycomicbus_cli.exe """ + URL + @"""
    mycomicbus_cli.exe """ + URL + @""" -o [OUTPUT_PATH]
    mycomicbus_cli.exe """ + URL + @""" -o [OUTPUT_PATH] -thread [THREAD_COUNT]
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
        //public static Microsoft.JScript.Vsa.VsaEngine Engine = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();
        public static Microsoft.JScript.Vsa.VsaEngine Engine = Microsoft.JScript.Vsa.VsaEngine.CreateEngine();// AndGetGlobalScope(true, new string[0]).engine;
        public static object EvalJScript(string JScript)
        {
            object Result = null;
            try
            {

                Result = Microsoft.JScript.Eval.JScriptEvaluate(JScript, Engine);
            }
            catch (Exception ex)
            {
                return ex.Message + "\n\r" + ex.StackTrace;
            }
            return Result;
        }
        static myinclude my = new myinclude();
        public static async Task Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
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

            string output_path = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].ToLower() == "-o" && i + 1 < args.Length)
                {
                    output_path = args[i + 1];
                    i++; // 跳過下一個參數，因為它是 -o 的值
                }
                if (args[i].ToLower() == "-thread" && i + 1 < args.Length)
                {
                    THREAD_COUNT = Convert.ToInt32(args[i + 1]);
                    i++; // 跳過下一個參數，因為它是 -thread 的值
                }
            }

            //如果 THREAD_COUNT < 1 則設定為 1
            if (THREAD_COUNT < 1)
            {
                //罵使用者不可以設定小於 1 的值
                echo("\n您指定的 -thread < 1 ，只好強定指定為 1 \n");
                THREAD_COUNT = 1;
            }


            //echo(URL);
            //exit();
            string data = my.b2s(my.file_get_contents(URL));
            //echo(data);            
            //exit();
            List<string> preScripts = new List<string>();
            //preScripts.Add("$=function(){};");
            preScripts.Add("var pp = '';var adsbygoogle=null;");
            //preScripts.Add("var window=null;");
            preScripts.Add("var localStorage={'getItem':function(){return '';}, 'setItem':function(a,b){} };");
            preScripts.Add("var y='46';");
            preScripts.Add("function spp(){};");
            preScripts.Add("var WWWWWTTTTTFFFFF='';");
            preScripts.Add("var document='';");
            //preScripts.Add("function $(p){};");
            preScripts.Add("function loadingpage(p){};");
            preScripts.Add("function initcomment(p){};");
            preScripts.Add("function mm(p){return (parseInt((p-1)/10)%10)+(((p-1)%10)*3)};");
            preScripts.Add("function nn(n){return n<10?'00'+n:n<100?'0'+n:n;};");
            preScripts.Add("function su(a,b,c){var e=(a+'').substring(b,b+c);return (e);};");
            preScripts.Add("function lc(l){ if (l.length != 2) return l; var az = \"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ\"; var a = l.substring(0,1); var b = l.substring(1, 2); if (a == \"Z\") return 8000 + az.indexOf(b); else return az.indexOf(a) * 52 + az.indexOf(b); };");
            preScripts.Add("function request(queryStringName){var returnValue=\"\";var URLString=new String(document.location);var serachLocation=-1;var queryStringLength=queryStringName.length;do{serachLocation=URLString.indexOf(queryStringName+\"\\= \");if (serachLocation!=-1){if ((URLString.charAt(serachLocation-1)=='?') || (URLString.charAt(serachLocation-1)=='&')){URLString=URLString.substr(serachLocation);break;}URLString=URLString.substr(serachLocation+queryStringLength+1);}}while (serachLocation!=-1)if (serachLocation!=-1){var seperatorLocation=URLString.indexOf(\" & \");if (seperatorLocation==-1){returnValue=URLString.substr(queryStringLength+1);}else{returnValue=URLString.substring(queryStringLength+1,seperatorLocation);} }return returnValue;}");
            string ch = "";
            //取得使用者要抓取的回數

            if (!my.is_string_like(URL, "ch="))
            {
                ch = "1";
            }
            else
            {
                ch = my.explode("-", my.explode("ch=", URL)[1])[0];//第幾回
            }
            Console.WriteLine("ch=" + ch);

            preScripts.Add("ch=" + ch);
            //merge all scripts 
            List<string> AllJS = new List<string>();
            var mjs = my.explode("<script>", data);
            for (int i = 6; i < mjs.Count(); i++)
            {
                var _m = my.explode("</script>", mjs[i]);
                AllJS.Add(_m[0]);
            }
            //var mScripts = my.explode("<script>", data)[7];
            //string scripts = my.explode("</script>", mScripts)[0].Trim();
            string scripts = my.implode("\n\n ", AllJS);


            //2024-08-31 8、修正如下載 https://a.twobili.com/online/new-7998.html?ch=2 ，程式會取得 https://a.twobili.com/online/new-7998.html?ch=1 的圖片網址，而不是第二集圖片的網址(感謝 actcathorse 回報)
            //奇怪，當時我為什麼要註解掉這行...Orz !?
            scripts = scripts.Replace("var pi=ch", "ch=" + ch + ";var pi=ch");


            //scripts = scripts.Replace("\n", " ").Replace("\r", " ");
            //echo("\nCounts:" + my.explode("<script>", data).Count().ToString() + "\n");
            //echo(scripts);
            //exit();
            scripts = scripts.Replace("document.getElementById(e)", "e");

            scripts = scripts.Replace("}var", "};var");
            //scripts = scripts.Replace("ge('TheImg').src=", "WWWWWTTTTTFFFFF=");
            scripts = scripts.Replace(").src=", ");WWWWWTTTTTFFFFF=");
            //scripts = scripts.Replace(";", ";\n");
            //scripts = scripts.Replace(";", ";\n");
            //scripts = scripts.Replace("adsbygoogle", "//adsbygoogle");
            preScripts.Add(scripts);
            preScripts.Add("ps+\"___\"+\"https:\"+WWWWWTTTTTFFFFF;");


            //preScripts.Add("swtvv;");
            string sc = my.implode("\n", preScripts);
            sc = sc.Replace("; ", "; \n");
            sc = sc.Replace("(adsbygoogl", "//(adsbygoogl");
            //echo(sc);
            //exit();

            sc = sc.Replace("if (localStorage.getItem(\"imgmode\")" + my.get_between(sc, "if (localStorage.getItem(\"imgmode\")", "document.getElementById(\"barcodeimg\");"), "");
            sc = sc.Replace("document.writeln(\"<scr" + my.get_between(sc, "document.writeln(\"<scr", "ipt>\");") + "ipt>\");", "");
            sc = sc.Replace("document.writeln(\"<scr" + my.get_between(sc, "document.writeln(\"<scr", "ipt>\");") + "ipt>\");", "");
            sc = sc.Replace("document.getElementById(\"barcodeimg\");", "");
            sc = sc.Replace("(function() {" + my.get_between(sc, "(function() {", "})();") + "})();", "");

            //my.file_put_contents("sc.txt", sc);
            string finalData = EvalJScript(sc).ToString().Trim();
            //echo(sc);
            //echo(finalData);
            //exit();
            var m = my.explode("___", finalData);
            int pages = Convert.ToInt32(m[0]);
            string output = "Totals:" + pages.ToString() + "\n";
            //string t = m[1].Replace("/001_", "/{PAGE}_");

            //t = t.Replace("/00_", "/{PAGE}_");
            //echo(t);
            //exit();
            for (int i = 1; i <= pages; i++)
            {
                //取得 WWWWWTTTTTFFFFF=...... 至 '.jpg';
                //WWWWWTTTTTFFFFF='//img'+su(yvdnl, 0, 1)+'.8comic.com/'+su(yvdnl,1,1)+'/'+ti+'/'+iyjco+'/'+ nn(p)+'_'+su(qvjme,mm(p),3)+'.jpg';
                //string d = t.Replace("{PAGE}", i.ToString().PadLeft(3, '0'));
                //output += d + "\n";
                var imgPath = "\"https:\"+" + my.get_between(sc, ";WWWWWTTTTTFFFFF=", ";");
                //echo(imgPath);
                //exit();
                string _sc = "";
                if (imgPath.IndexOf("(p)") != -1)
                {
                    _sc = sc + "\n" + imgPath.Replace("(p)", "(" + i + ")") + ";";
                }
                else
                {
                    _sc = sc + "\n" + imgPath.Replace("(pp)", "(" + i + ")") + ";";
                }


                output += EvalJScript(_sc).ToString() + "\n";
            }
            echo(output);

            if (output_path == null)
            {
                exit();
            }

            //需下載
            if (!my.is_dir(output_path))
            {
                my.mkdir(output_path);
            }

            //下面這段程式碼是用來下載圖片的
            //改成支援多執行緒下載
            Dictionary<int, string> downloadList = new Dictionary<int, string>();
            for (int i = 1; i <= pages; i++)
            {
                var imgPath = "\"https:\"+" + my.get_between(sc, ";WWWWWTTTTTFFFFF=", ";");
                string _sc = "";
                if (imgPath.IndexOf("(p)") != -1)
                {
                    _sc = sc + "\n" + imgPath.Replace("(p)", "(" + i + ")") + ";";
                }
                else
                {
                    _sc = sc + "\n" + imgPath.Replace("(pp)", "(" + i + ")") + ";";
                }
                string imgURLPath = EvalJScript(_sc).ToString();
                downloadList.Add(i, imgURLPath);
            }

            //開始下載
            await my.DownloadImages(downloadList, output_path, THREAD_COUNT);

            /*
            for (int i = 1; i <= pages; i++)
            {
                //取得 WWWWWTTTTTFFFFF=...... 至 '.jpg';
                //WWWWWTTTTTFFFFF='//img'+su(yvdnl, 0, 1)+'.8comic.com/'+su(yvdnl,1,1)+'/'+ti+'/'+iyjco+'/'+ nn(p)+'_'+su(qvjme,mm(p),3)+'.jpg';
                //string d = t.Replace("{PAGE}", i.ToString().PadLeft(3, '0'));
                //output += d + "\n";
                var imgPath = "\"https:\"+" + my.get_between(sc, ";WWWWWTTTTTFFFFF=", ";");
                //echo(imgPath);
                //exit();
                var _sc = sc + "\n" + imgPath.Replace("(p)", "(" + i + ")") + ";";
                string imgURLPath = EvalJScript(_sc).ToString();
                string CMD = "binary\\wget.exe \"" + imgURLPath + "\" -O \"" + output_path + "\\" + i.ToString() + ".jpg\"";
                echo(CMD);
                my.system(CMD);
            }
            */
        }
    }
}
