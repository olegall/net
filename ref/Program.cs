void SetName(ref A a) // ссылка на ссылку. без ref предупреждение на последней строке и она не повлияет
{
    a.Id = 0;
    a.Name = null;

    // созд-ся новый объект, a иниц-ся его адресом.
    // без ref: отрыв от 1-го объекта, изменения на него не повлияют.
    // c ref: подкапотная ссылка, указывает сначала на изначальный a, потом на изменённый a TODO
    a = new A{ Id = 2, Name = "Pete"}; 
}

var a = new A{ Id = 1, Name = "Oleg"};
SetName(ref a);
int break_ = 0;

class A
{
    public int Id { get; set; }
    public string Name { get; set; }
}
