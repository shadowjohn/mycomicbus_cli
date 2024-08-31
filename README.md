# mycomicbus_cli
<h2>cli 模式抓 8 comicbus 漫畫 方法</h2>
<h3>Author : 羽山 https://3wa.tw/</h3>
<h3>Version : 2024-08-31</h3>
<br>
<img src="snapshot\01.png">
直接下語法抓 comicbus 8comic 漫畫列表

<br>
<h3>使用方法：</h3>

mycomicbus_cli.exe<br>
mycomicbus_cli.exe test<br>
mycomicbus_cli.exe "https://comicabc.com/online/new-18838.html?ch=1"<br>
mycomicbus_cli.exe "https://comicabc.com/online/new-18838.html?ch=1" -o [output_path]<br>

2024-08-31 支援多線程下載( N >= 1)，太多線程你可能會被網站 ban...呵呵 
一次抓5筆很爽呦~咻咻咻~<br>
mycomicbus_cli.exe "https://comicabc.com/online/new-18838.html?ch=1" -o [output_path] -thread N<br>
<br>

<h3>回應：</h3>
Totals:70<br>
https://img8.8comic.com/3/18838/1/001_8Nk.jpg<br>
https://img8.8comic.com/3/18838/1/002_337.jpg<br>
https://img8.8comic.com/3/18838/1/003_Duc.jpg<br>
https://img8.8comic.com/3/18838/1/004_9S3.jpg<br>
https://img8.8comic.com/3/18838/1/005_58c.jpg<br>
https://img8.8comic.com/3/18838/1/006_4d9.jpg<br>
https://img8.8comic.com/3/18838/1/007_GWR.jpg<br>
https://img8.8comic.com/3/18838/1/008_GsQ.jpg<br>
...<br>
...<br>
<h3>下載範例：</h3>
binary\wget.exe "https://img4.8comic.com/4/17779/1/001_NT2.jpg" -O 001.png
<br>
<br>
<h3>編譯環境：</h3>
VisualStudio 2017
<br>
<br>
<h3>相依套件：</h3>
<ul>
  <li>Microsoft.JScript</li>
</ul>
<br>
<br>
<h3>版本歷程：</h3>
2024-08-31
  1. 7、增加多線程下載 -thread 預設為 1，只有在 -o 模式才會啟動多線程下載
  2. 8、修正如下載 https://a.twobili.com/online/new-7998.html?ch=2 ，程式會取得 https://a.twobili.com/online/new-7998.html?ch=1 的圖片網址，而不是第二集圖片的網址(感謝 actcathorse 回報)

2023-09-06 
  1. 因為無法下載，經檢查是 UserAgent 過舊且格式寫錯，修正了<br>
  2. 增加下載的方法 -o output_path<br>
  3. 修正一拳超人也可以下載 https://a.twobili.com/ReadComic/10406/1/1TQ_J0423K6.html<br>
2021-10-05 官方多塞了一行 (adsbygoogle = window.adsbygoogle || []).push({});<br>
加上註解後即排除<br>
<br>
2021-09-20 好像換新網址了<br>
mycomicbus_cli.exe "https://comicabc.com/online/new-18838.html?ch=1"<br>
<br>
<h3>ToDo：</h3>
<ul>
  <li>(Done 2021-07-06) 1、解圖演算法更新，網站加了  localStorage 語法，相容性修正</li>
  <li>(Done 2021-09-20) 2、換網址了</li>
  <li>(Done 2021-10-05) 3、官方偷塞了一行 (adsbygoogle = window.adsbygoogle || []).push({});</li>
  <li>(Done 2023-09-06) 4、更新 UserAgent</li>
  <li>(Done 2023-09-06) 5、增加下載的方法 -○ output_path</li>
  <li>(Done 2023-09-06) 6、修正一拳超人也可以下載 https://a.twobili.com/ReadComic/10406/1/1TQ_J0423K6.html</li>
  <li>(Done 2024-08-31) 7、增加多線程下載 -thread 預設為 1，只有在 -o 模式才會啟動多線程下載</li>  
  <li>(Done 2024-08-31) 8、修正如下載 https://a.twobili.com/online/new-7998.html?ch=2 ，程式會取得 https://a.twobili.com/online/new-7998.html?ch=1 的圖片網址，而不是第二集圖片的網址(感謝 actcathorse 回報)</li>
</ul>