// Что произойдёт при запуске программы? 0 1+ 41 42

int a = 0;

int Foo() 
{ 
    a = a + 41; 
    return 1;
}

a += Foo(); // TODO что это?
Console.WriteLine(a);