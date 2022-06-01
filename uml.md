### uml: project po
```plantuml
@startuml
    class App{
        + Workplaces
        + Workers
    }
    App *-- Workplace
    App *-- Worker
    class TimeInterval {
        + start
        + end
        + duration
    }
    class TimeIntervals{
    }

    class Shift{
        + required_qualifications
        + assigned_worker
    }

    TimeInterval --* TimeIntervals 
    Shift ^-- TimeInterval


    class Person{
        + firstname
        + lastname
    }

    class Worker {
        + qualifications
        + availibility
        + assigned_shifts
        + (current availability)
    }
    Worker *--^ Shift
    Worker *-- TimeIntervals
    Worker ^-- Person

    class TimeTable{
        + shifts
    }
    TimeTable *-- Shift

    class Workplace{
        + timetable
        + workers
    }
    Workplace *-- Worker
    Workplace *-- TimeTable

    class Qualification{
        + description
    }

    Worker *-- Qualification
    Shift *-- Qualification



@enduml