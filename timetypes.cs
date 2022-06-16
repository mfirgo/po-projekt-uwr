using System;
using System.Collections.Generic;
class TimeInterval: IEquatable<TimeInterval>{
    private DateTime start;
    private DateTime end;
    public TimeInterval(DateTime start, DateTime end){
        if (start<=end){
            this.start = start;
            this.end = end;
        } else {
            Console.WriteLine("Warning: start is after end");
            this.end = start;
            this.start = end;
        }
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
    public override string ToString() { 
        return start.ToString() + " - " + end.ToString();
    }
    public bool Equals(TimeInterval other){
        return this.Start==other.Start && this.End == other.End;
    }
    public bool contains(TimeInterval other){
        return this.Start<=other.Start && this.End>=other.End;
    }
    public bool intersects(TimeInterval other){
        return this.End>=other.Start || this.Start<=other.End;
    }
    public TimeInterval add(TimeInterval other){
        if (!this.intersects(other)){
            throw new ArgumentException("Arguments must intersect");
        }
        return new TimeInterval(this.Start<other.Start ? this.Start : other.Start,
                                this.End>other.End ? this.End : other.End);
    }
    // public TimeInterval subtract(TimeInterval other){
    //     return new TimeInterval(this.Start)
    // }
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
    public void add(TimeInterval interval_to_add){
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
}

// class Shift : TimeInterval{
//     // information about place / qualifications of worker needed

// }

// class Shifts{
//     // collection of shifts
// }

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
        intervals.add(interval1);
        Console.WriteLine(intervals.ToString());
        intervals.add(interval2);
        Console.WriteLine(intervals.ToString());
        intervals.add(interval3);
        Console.WriteLine(intervals.ToString());
        intervals.add(interval4);
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