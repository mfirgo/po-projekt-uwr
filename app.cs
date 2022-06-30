using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
class App{
    private List<Worker> workers;
    private List<Workplace> workplaces;
    public ReadOnlyCollection<Worker> Workers{
        get {return this.workers.AsReadOnly();}
    }
    public ReadOnlyCollection<Workplace> Workplaces{
        get {return this.workplaces.AsReadOnly();}
    }
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

        this.workers[1].AddQualification("Barista");
        this.workers[2].AddQualification("Waiter");
        this.workers[3].AddQualification("Cleaner");
        this.workers[4].AddQualification("TeaMaster");
        this.workers[5].AddQualifications(new List<String>(){"Waiter", "Cleaner"});
        this.workers[6].AddQualifications(new List<String>(){"Barista", "TeaMaster"});
        this.workers[7].AddQualifications(new List<String>(){"Waiter", "Cleaner", "Barista", "TeaMaster"});

        this.workers[0].Availability.Add(new TimeInterval("04/06/2022 12:00:00", "04/06/2022 18:00:00"));
        this.workers[0].Availability.Add(new TimeInterval("04/07/2022 8:00:00", "04/07/2022 14:00:00"));

        this.workers[1].Availability.Add(new TimeInterval("04/06/2022 8:00:00", "04/06/2022 20:00:00"));
        this.workers[2].Availability.Add(new TimeInterval("04/06/2022 8:00:00", "04/06/2022 20:00:00"));
        this.workers[3].Availability.Add(new TimeInterval("04/06/2022 8:00:00", "04/06/2022 20:00:00"));
        this.workers[4].Availability.Add(new TimeInterval("04/06/2022 8:00:00", "04/06/2022 20:00:00"));
        this.workers[5].Availability.Add(new TimeInterval("04/06/2022 8:00:00", "04/06/2022 20:00:00"));

        this.workers[6].Availability.Add(new TimeInterval("04/06/2022 8:00:00", "04/06/2022 14:00:00"));
        this.workers[6].Availability.Add(new TimeInterval("04/06/2022 12:00:00", "04/06/2022 14:00:00"));
        this.workers[6].Availability.Add(new TimeInterval("04/06/2022 14:00:00", "04/06/2022 16:00:00"));
        this.workers[6].Availability.Add(new TimeInterval("04/07/2022 12:00:00", "04/07/2022 18:00:00"));

        this.workers[7].Availability.Add(new TimeInterval("04/06/2022 6:00:00", "04/07/2022 20:00:00"));


    }
    public void create_sample_workplaces(){
        this.workplaces.Add(new Workplace("Morning Coffee"));
        this.workplaces.Add(new Workplace("Afternoon Tea"));
        this.workplaces.Add(new Workplace("Evening Cocoa"));

        this.workplaces[0].addWorker(this.workers[0]);
        this.workplaces[0].addWorker(this.workers[1]);
        this.workplaces[0].addWorker(this.workers[2]);
        this.workplaces[0].addWorker(this.workers[5]);

        this.workplaces[1].addWorker(this.workers[4]);
        this.workplaces[1].addWorker(this.workers[6]);
        this.workplaces[1].addWorker(this.workers[0]);
        this.workplaces[1].addWorker(this.workers[3]);

        this.workplaces[2].addWorker(this.workers[1]);
        this.workplaces[2].addWorker(this.workers[0]);
        this.workplaces[2].addWorker(this.workers[7]);

        this.workplaces[0].addShift("04/06/2022 8:00:00", "04/06/2022 10:00:00", new List<String>());
        this.workplaces[0].addShift("04/06/2022 8:00:00", "04/06/2022 10:00:00", new List<String>(){"Barista"});

    }

    public App(){
        this.init_lists();
    }
}

class TestProgram{
    public static void Main(){
        App app = new App();
        app.create_sample_data();
        Console.WriteLine("-- Workers --");
        foreach(Worker w in app.Workers){
            Console.WriteLine(w);
        }
        Console.WriteLine("-- Workplaces --");
        foreach(Workplace w in app.Workplaces){
            Console.WriteLine(w);
        }
        Console.WriteLine("-- Details on workers --");
        Console.WriteLine();
        foreach(Worker worker in app.Workers){
            Console.Write(worker+"\t");
            foreach(Qualification q in worker.Qualifications){
                Console.Write(q.ToString() + " ");
            }
            Console.WriteLine();
            Console.Write(worker.Availability);
            Console.WriteLine();
        }
        
        Console.WriteLine("-- Shifts --");
        Shift s0 = app.Workplaces[0].Shifts[0];
        Shift s1 = app.Workplaces[0].Shifts[1];
        Console.WriteLine(s0);
        Console.Write("Possible Workers:\t"); foreach(Worker w in s0.possibleWorkers()){Console.Write(w.initials+" ");}
        Console.WriteLine();
        Console.WriteLine(s1);
        Console.Write("Possible Workers:\t");
        foreach(Worker w in s1.possibleWorkers()){Console.Write(w.initials+" ");}
        Console.WriteLine();
        s1.AssignWorker(s1.possibleWorkers()[0]);
        Console.WriteLine("Assigned "+ s1);
        Console.WriteLine(s0);
        Console.Write("Possible Workers:\t");
        foreach(Worker w in s0.possibleWorkers()){Console.Write(w.initials+" ");}
        Console.WriteLine();
        s0.AssignWorker(s0.possibleWorkers()[0]);
        Console.WriteLine("Assigned "+ s0);




    }
}