using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
class TimeInterval: IEquatable<TimeInterval>{
    private DateTime start;
    private DateTime end;
    protected void init(DateTime start, DateTime end){
        // if (start==end){
        //     throw new ArgumentException("start must be different than end");
        // }
        if (start<=end){
            this.start = start;
            this.end = end;
        } else {
            Console.WriteLine("Warning: start is after end");
            this.end = start;
            this.start = end;
        }
    }
    // public TimeInterval(){
    //     this.start = null;
    //     this.end = null;
    // }
    public TimeInterval(DateTime start, DateTime end){
        this.init(start, end);
    }
    public DateTime Start{
        get{ return start;}
    }
    public DateTime End{
        get{ return end;}
    }
    public TimeSpan Duration {
        get { return end - start; }
    }
    public bool isZeroLength(){
        return this.Duration.Equals(TimeSpan.Zero);
    }
    public override string ToString() { 
        return start.ToString() + " - " + end.ToString();
    }
    public bool Equals(TimeInterval other){
        return this.Start==other.Start && this.End == other.End;
    }
    public bool contains(TimeInterval other){
        return this.Start<=other.Start && this.End>=other.End;
    }
    public bool containsFully(TimeInterval other){
        return this.Start<other.Start && this.End>other.End;
    }
    public bool intersects(TimeInterval other){
        return this.End>=other.Start || this.Start<=other.End;
    }
    // public bool intersectsLeft(TimeInterval other){
    //     return other.Start<this.End && this.End<other.End;
    // }
    // public bool intersectsRight(TimeInterval other){
    //     return other.intersectsRight(this);
    // }
    public TimeInterval add(TimeInterval other){
        if (!this.intersects(other)){
            throw new ArgumentException("Arguments must intersect");
        }
        return new TimeInterval(this.Start<other.Start ? this.Start : other.Start,
                                this.End>other.End ? this.End : other.End);
    }
    // not needed - we only use subtraction for timeintervals that contain one another
    // public TimeInterval subtract(TimeInterval other){
    //     if (other.contains(this)){
    //         return 
    //     } else if (this.intersectsLeft(other)){
    //         return new TimeInterval(other.End, this.End);
    //     } else if(this.intersectsRight(other)){
    //         return new TimeInterval(this.Start, this.End);
    //     }
    //     return new TimeInterval(this.Start<other.Start ? this.Start: other.End,
    //                             this.End<)
    // }
    public Tuple<TimeInterval,TimeInterval> Split(TimeInterval other){
        if(!this.contains(other)){
            throw new ArgumentException("other timeinterval must be contained in this time interval");
        }
        return Tuple.Create(new TimeInterval(this.Start, other.Start),
                          new TimeInterval(other.End, this.end));
        // if (!this.containsFully(other)){
        //     throw new ArgumentException("Other time interval must be fully contained within this time interval");
        // }
        // return new Tuple(new TimeInterval(this.Start, other.Start),
        //                  new TimeInterval(other.End, this.end));
    }
}
// TODO:
// add (with merging where it is possible)
// contains with time interval
// substract (time interval)
// 
class TimeIntervals{
// collection of Time Intervals
    //private interval_list
    private List<TimeInterval> interval_list;
    public TimeIntervals(){
        this.interval_list = new List<TimeInterval>();
    }
    public override string ToString(){
        String interval_string = "TimeIntervals:\n";
        foreach (TimeInterval interval in this.interval_list){
            interval_string += interval.ToString() + "\n";
        }
        return interval_string;
    }
    public void Add(TimeInterval interval_to_add){
        if (interval_to_add.isZeroLength()){
            return;
        }
        List<TimeInterval> intervals_to_remove = new List<TimeInterval>();
        foreach(TimeInterval interval in this.interval_list){
            if (interval_to_add.intersects(interval)){
                intervals_to_remove.Add(interval);
                interval_to_add = interval_to_add.add(interval);
            }
        }
        foreach(TimeInterval interval in intervals_to_remove){
            this.interval_list.Remove(interval);
        }
        this.interval_list.Add(interval_to_add);
    }
    public bool contains(TimeInterval other){
        foreach(TimeInterval interval in this.interval_list){
            if (interval.contains(other)){
                return true;
            }
        }
        return false;
    }
    public void subtract(TimeInterval other){
        if (other.isZeroLength()){
            return;
        }
        TimeInterval interval = null;
        for (int index=0; index<this.interval_list.Count; index++){
            interval = this.interval_list[index];
            if( interval.contains(other)){
                break;
            }
        }
        if (interval == null){
            throw new ArgumentException("no interval containing "+other.ToString()+" was found");
        }
        Tuple<TimeInterval,TimeInterval> split_result = interval.Split(other);
        this.interval_list.Remove(interval);
        if(!split_result.Item1.isZeroLength()){
            this.interval_list.Add(split_result.Item1);
        }
        if(!split_result.Item2.isZeroLength()){
            this.interval_list.Add(split_result.Item2);
        }   
    }
}

class Shift : TimeInterval{
    // information about place / qualifications of worker needed
    private Worker assignedWorker;
    private List<Qualification> qualifications;
    public Shift(DateTime start, DateTime end, List<String> qualifications_names): base(start, end){
        //this.init(start, end);
        this.assignedWorker = null;
        qualifications = new List<Qualification>();
        foreach (String name in qualifications_names){
            qualifications.Add(Qualification.GetInstance(name));
        }
    }
    public ReadOnlyCollection<Qualification> Qualifications{
        get {return this.qualifications.AsReadOnly();}
    }
    public void AssignWorker(Worker worker){
        if (this.isAssigned(worker)){
            return;
        }
        worker.AssignShift(this);
        this.assignedWorker = worker;
    }
    public bool isAssigned(Worker worker){
        return this.assignedWorker.Equals(worker);
    }
    public Worker AssignedWorker{
        get{return assignedWorker;}
    }
    public void unAssignWorker(Worker worker){
        if (this.assignedWorker.Equals(worker)){
            this.assignedWorker = null;
            worker.unAssignShift(this);
        }
    }
}

class TimeTest{
    static TimeInterval interval1;
    static TimeInterval interval2;
    static TimeInterval interval3;
    static TimeInterval interval4;
    private static void init_variables(){
        interval1 = new TimeInterval(new DateTime(2022, 6, 20, 8,0,0), new DateTime(2022, 6, 20, 10,0,0));
        interval2 = new TimeInterval(new DateTime(2022, 6, 20, 10,0,0), new DateTime(2022, 6, 20, 12,0,0));
        interval3 = new TimeInterval(new DateTime(2022, 6, 20, 8,0,0), new DateTime(2022, 6, 20, 20,0,0));
        interval4 = new TimeInterval(new DateTime(2022, 6, 20, 6,0,0), new DateTime(2022, 6, 20, 10,0,0));
    }
    private static void print_intervals(){
        Console.WriteLine("interval1: " + interval1.ToString());
        Console.WriteLine("interval2: " + interval2.ToString());
        Console.WriteLine("interval3: " + interval3.ToString());
        Console.WriteLine("interval4: " + interval4.ToString());
    }
    private static void comparison_test(){
        Console.WriteLine(interval1.Equals(new TimeInterval(new DateTime(2022, 6, 20, 8,0,0), new DateTime(2022, 6, 20, 10,0,0))));
        Console.WriteLine(new DateTime(2022, 6, 20, 8,0,0) == new DateTime(2022, 6, 20, 8,0,0));
    }
    private static void add_timeinterval_test(){
        Console.WriteLine("interval1+interval2 = " + interval1.add(interval2).ToString());
        if (interval1.add(interval2) != new TimeInterval(new DateTime(2022, 6, 20, 8,0,0),
                                                         new DateTime(2022, 6, 20, 12,0,0) )){
            Console.WriteLine("ERROR");
        }
    }
    private static void add_timeintervals_test(){
        TimeIntervals intervals = new TimeIntervals();
        Console.WriteLine(intervals.ToString());
        intervals.Add(interval1);
        Console.WriteLine(intervals.ToString());
        intervals.Add(interval2);
        Console.WriteLine(intervals.ToString());
        intervals.Add(interval3);
        Console.WriteLine(intervals.ToString());
        intervals.Add(interval4);
        Console.WriteLine(intervals.ToString());
    }

    public static void Main(){
        init_variables();
        print_intervals();
        comparison_test();
        add_timeinterval_test();
        add_timeintervals_test();
    }
}