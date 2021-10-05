# mycomicbus_cli
<h2>cli 模式抓 8 comicbus 漫畫 方法</h2>
<h3>Author : 羽山 https://3wa.tw/</h3>
<h3>Version : 2021-10-05</h3>
<br>
<img src="snapshot\01.png">
直接下語法抓 comicbus 8comic 漫畫列表


<br>
<h3>使用方法：</h3>

mycomicbus_cli.exe<br>
mycomicbus_cli.exe test<br>
mycomicbus_cli.exe "https://comicbus.live/online/a-18838.html?ch=1"<br>
<br>

<h3>回應：</h3>
Totals:70<br>
https://img4.8comic.com/4/17779/1/001_NT2.jpg<br>
https://img4.8comic.com/4/17779/1/002_MjH.jpg<br>
https://img4.8comic.com/4/17779/1/003_58V.jpg<br>
https://img4.8comic.com/4/17779/1/004_BDH.jpg<br>
https://img4.8comic.com/4/17779/1/005_cb9.jpg<br>
https://img4.8comic.com/4/17779/1/006_Uv4.jpg<br>
https://img4.8comic.com/4/17779/1/007_7uj.jpg<br>
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
2021-10-05 官方多塞了一行 (adsbygoogle = window.adsbygoogle || []).push({});<br>
加上註解後即排除<br>
<br>
2021-09-20 好像換新網址了<br>
mycomicbus_cli.exe "https://comicabc.com/online/new-18838.html?ch=1"<br>
<br>
<h3>ToDo：</h3>
<ul>
  <li>(Done 2021-07-06)1、解圖演算法更新，網站加了  localStorage 語法，相容性修正</li>
  <li>(Done 2021-09-20)2、換網址了</li>
  <li>(Done 2021-10-05)3、官方偷塞了一行 (adsbygoogle = window.adsbygoogle || []).push({});</li>
</ul>