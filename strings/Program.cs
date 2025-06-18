// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
// TODO
var a1 = "a";
a1 = "b"; // создаться новая строка и на изначальную не будет ссылок? или просто изменится значение?

var a2 = "a";
a2 += "_"; // создаться новая строка и на изначальную не будет ссылок?

// когда пересоздаётся строка? когда добавляется хотя бы символ? (меняется длина в байт)

#region
//https://ru.stackoverflow.com/questions/875354/%D0%9A%D0%B0%D0%BA-%D1%80%D0%B0%D0%B1%D0%BE%D1%82%D0%B0%D0%B5%D1%82-%D0%B8%D0%BD%D1%82%D0%B5%D1%80%D0%BD%D0%B8%D1%80%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-%D1%81%D1%82%D1%80%D0%BE%D0%BA
//string a = "aaa";
//string b = "aaa";
//bool c = (object)a == (object)b; // И получаем true 

//string a = "aaa";
//string b = "aa";
//b += "a";
//bool c = (object)a == (object)b; // И получаем false

//string a = "aaa";
//string b = "aa";
//b += "a";
//b = string.Intern(b);
//bool c = (object)a == (object)b; // True
#endregion