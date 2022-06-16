class Person{
    protected string _firstName;
    protected string _lastName;

    public string firstName{
        get { return _firstName; }
    }
    public string lastName{
        get { return _lastName; }
    }
    public string initials{
        get { return _firstName.Substring(0,1)+_lastName.Substring(0,2);}
    }
    public Person(string firstName, string lastName){
        this._firstName = firstName;
        this._lastName = lastName;
    }
}
class Worker{
    
}