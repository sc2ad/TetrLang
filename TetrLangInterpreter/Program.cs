using TetrLang;

//var testFile = @"D:\Stuffs\TetrReplays\0f_fun_stuff.ttr";
var testFile = @"D:\Code\TetrLang\a_test.ttr";
using var fs = File.OpenRead(testFile);
if (Parser.TryParseFile(fs, out var repl))
{
    foreach (var ev in repl!.Data!.Events)
    {
        Console.WriteLine(ev);
    }
}
var inter = new Interpreter(repl!, Console.WriteLine);
inter.Run();
