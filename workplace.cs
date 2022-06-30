using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
class Workplace{
    private List<Worker> workers;
    private List<Shift> shifts;
    private String name;
    public Workplace(String name){
        this.name = name;
        this.workers = new List<Worker>();
        this.shifts = new List<Shift>();
    }
    public String Name{
        get{return this.name;}
    }
    public void addWorker(Worker worker){
        this.workers.Add(worker);
    }
    // I dont want the same shift to be assigned in several workplaces so shift is created by workplace
    public void addShift(DateTime start, DateTime end, List<String> qualifications_names){
        this.shifts.Add(new Shift(start, end, qualifications_names, this));
    }
    public void addShift(String start, String end, List<String> qualifications_names){
        this.shifts.Add(new Shift(DateTime.Parse(start), DateTime.Parse(end), qualifications_names, this));
    }
    public ReadOnlyCollection<Shift> Shifts{
        get {return this.shifts.AsReadOnly();}
    }
    public ReadOnlyCollection<Worker> Workers{
        get {return this.workers.AsReadOnly();}
    }
    public override String ToString(){
        String result = this.name + "\t- workers: ";
        foreach(Worker w in this.workers){
            result+= w.initials + " ";
        }
        return result;
    }
}
class Workplace_tests{
    static void Main(){
        Workplace workplace = new Workplace("Test_workplace");
        Console.WriteLine(workplace.Name);
        Worker worker = new Worker("Tom", "Cruise");
        workplace.addWorker(worker);
        foreach (Worker w in workplace.Workers){
            Console.WriteLine(w.ToString());
            Console.WriteLine(w);
        }
        Console.WriteLine(string.Join("\n", workplace.Workers[0].Shifts));
        workplace.addShift(new DateTime(2022, 6, 20, 8,0,0), new DateTime(2022, 6, 20, 10,0,0), new List<String>());
        Console.WriteLine(string.Join("\n", workplace.Shifts));
        workplace.Workers[0].Availability.Add( new TimeInterval(new DateTime(2022, 6, 20, 8,0,0), new DateTime(2022, 6, 20, 20,0,0)));
        Console.WriteLine(workplace.Workers[0].Availability);
        workplace.Workers[0].AssignShift(workplace.Shifts[0]);
        Console.WriteLine(workplace.Workers[0].Availability);
        Console.WriteLine(string.Join("\n", workplace.Workers[0].Shifts));
        Console.WriteLine(string.Join("\n", workplace.Shifts));



    }
}