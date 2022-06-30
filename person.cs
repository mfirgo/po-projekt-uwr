using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
class Person{
    protected string _firstName;
    protected string _lastName;

    public string firstName{
        get { return _firstName; }
    }
    public string lastName{
        get { return _lastName; }
    }
    public string fullName{
        get{ return _firstName + " "+ _lastName;}
    }
    public string initials{
        get { return _firstName.Substring(0,1)+_lastName.Substring(0,2);}
    }
    public Person(string firstName, string lastName){
        this._firstName = firstName;
        this._lastName = lastName;
    }
    public override String ToString(){
        return this.initials+": "+this.firstName+" "+this.lastName;
    }
}
class Worker: Person{
    private TimeIntervals availability;
    private List<Qualification> qualifications;
    private List<Shift> shifts;
    public Worker(string firstName, string lastName) : base(firstName, lastName){
        this.availability = new TimeIntervals();
        this.qualifications = new List<Qualification>();
        this.shifts = new List<Shift>();
    }
    // public override String toString(){
    //     return base.toString();
    // }
    public void AddQualification(Qualification q){
        this.qualifications.Add(q);
    }
    public void AddQualification(String q){
        this.qualifications.Add(Qualification.GetInstance(q));
    }
    public void AddQualifications(List<String> qualifications){
        foreach(String q in qualifications){
            this.AddQualification(q);
        }
    }
    public void AddQualifications(List<Qualification> qualifications){
        foreach(Qualification q in qualifications){
            this.AddQualification(q);
        }
    }
    public ReadOnlyCollection<Qualification> Qualifications{
        get{return this.qualifications.AsReadOnly();}
    }
    public ReadOnlyCollection<Shift> Shifts{
        get{return this.shifts.AsReadOnly();}
    }
    public TimeIntervals Availability{
        get {return availability;}
    }
    public bool hasQualifications(Shift shift){
        foreach(Qualification q in shift.Qualifications){
            if (!this.qualifications.Contains(q)){
                return false;
            }
        }
        return true;
    }
    public bool isAvailable(TimeInterval interval){
        return this.availability.contains(interval);
    }
    public bool canTakeShift(Shift shift){
        return this.isAvailable(shift) && this.hasQualifications(shift);
    }
    public bool isAssigned(Shift shift){
        return this.shifts.Contains(shift);
    }
    public void AssignShift(Shift shift){
        if (this.isAssigned(shift)){
            return;
        } else if (this.canTakeShift(shift)){
            try{
                this.availability.subtract(shift);
                shifts.Add(shift);
                shift.AssignWorker(this);
            } catch (ArgumentException e){
                throw e;
            }
        } else{
            throw new ArgumentException("Worker "+this.initials+" can't take shift "+shift.ToString());
        }
    }
    public void unAssignShift(Shift shift){
        if(this.isAssigned(shift)){
            this.shifts.Remove(shift);
            this.availability.Add(shift);
            shift.unAssignWorker(this);
        }
    }
}