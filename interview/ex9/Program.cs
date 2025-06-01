Dog dog1 = new Dog();
Animal dog2 = new Dog();
// Не виртуальный метод - вызовется метод класса, указанного у переменной 
dog1.Info(); // напишет Dog
dog2.Info(); // напишет Animal
// Виртуальный метод - вызовется метод класса, которого переменная реально имеет
dog1.Say(); // напишет Woof
dog2.Say(); // напишет Woof

Console.WriteLine("--------------");

Animal[] animals = new Animal[2];
animals[0] = new Cat();
animals[1] = new Dog();
FillAnimals(animals); // заполним массив вперемешку собаками и кошками
void FillAnimals(Animal[] animals)
{
    foreach (var animal in animals) 
        animal.Say(); // вызовется правильный метод
        //animal.Info(); // вызовется правильный метод
    // У невиртуальных методов так сделать нельзя! Полиморфизм в действии
}

class Animal {
    public void Info() { Console.WriteLine("Animal"); }
    public virtual void Say() { Console.WriteLine("Nothing to say"); }
}
    
class Cat : Animal {
    public void Info() { Console.WriteLine("Cat"); }
    public override void Say() { Console.WriteLine("Meow"); }
}
    
class Dog : Animal {
    public void Info() { Console.WriteLine("Dog"); }
    public override void Say() { Console.WriteLine("Woof"); }
}

