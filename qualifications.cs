using System;
using System.Collections.Generic;
class Qualification{
    private static Dictionary<String,Qualification> instances =
        new Dictionary<String, Qualification>();

    private String name;
    private String description;
    public String Name{
        get{return this.name;}
    }
    public String Description{
        get{return this.description;}
        //set{this.description = value}
    }
    private Qualification(String name, String description) {
        this.name = name;
        this.description = description;
    }
    public static Qualification GetInstance(String name){
        if (!instances.TryGetValue(name, out var instance)){
            throw new ArgumentException("Qualification '"+name+"' does not exist");
        }
        return instance;
    }
    public static void AddQualification(String name){
        AddQualification(name, "");
    }
    public static void AddQualification(String name, String description){
        if (instances.ContainsKey(name)){
            throw new ArgumentException("Qualification of name '"+name+"' already exists");
        }
        instances.Add(name, new Qualification(name, description));
    }
    public static Dictionary<String,Qualification>.KeyCollection Names{
        get {return instances.Keys;} //KeyCollection is readonly
    }
    public static String NamesAsString{
        get {
            String result = "";
            foreach(String name in instances.Keys){
                result+=name+"\n";
            }
            return result;
        }
    }
}

class Qualifications_tests{
    public static void Main(){
        Console.WriteLine(Qualification.NamesAsString);
        Qualification.AddQualification("barista", "can make coffe");
        Qualification.AddQualification("waiter", "waits the tables");
        Qualification.AddQualification("animator", "entertains people");
        Qualification barista = Qualification.GetInstance("barista");
        Console.WriteLine(barista.Name);
        Console.WriteLine(barista.Description);
        Console.WriteLine(Qualification.NamesAsString);
    }
}