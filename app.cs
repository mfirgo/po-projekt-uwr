using System.Collections.Generic;
using System;
class App{
    private List<Worker> workers;
    private List<Workplace> workplaces;
    private void init_lists(){
        this.workers = new List<Worker>();
        this.workplaces = new List<Workplace>();
    }
    public void create_sample_data(){
        this.create_sample_qualifications();
        this.create_sample_workers();
        this.create_sample_workplaces();
    }
    public void create_sample_qualifications(){
        Qualification.AddQualification("Barista", "Makes coffee");
        Qualification.AddQualification("Waiter", "Waits the tables");
        Qualification.AddQualification("Cleaner", "Responsible for cleaning");
        Qualification.AddQualification("TeaMaster", "Prepares the tea");
    }
    public void create_sample_workers(){
        this.workers.Add(new Worker("Aaron", "Anderson"));
        this.workers.Add(new Worker("Bob", "Best"));
        this.workers.Add(new Worker("Cecylia", "Clark"));
        this.workers.Add(new Worker("Damian", "Dust"));
        this.workers.Add(new Worker("Edward", "Ender"));
        this.workers.Add(new Worker("Fiona", "Fidlemaster"));
        this.workers.Add(new Worker("Georgia", "Grey"));
        this.workers.Add(new Worker("Hannah", "Hist"));

        Console.WriteLine(this.workers[2].ToString());
        this.workers[1].AddQualification("Barista");
        this.workers[2].AddQualifications(new List<String>(){"Waiter", "Cleaner"});
    }
    public void create_sample_workplaces(){
        this.workplaces.Add(new Workplace("Morning Coffee"));
        this.workplaces.Add(new Workplace("Afternoon Tea"));
        this.workplaces.Add(new Workplace("Evening Cocoa"));
    }

    public App(){
        this.init_lists();
    }
}

class TestProgram{
    public static void Main(){
        App app = new App();
        app.create_sample_data();
    }
}