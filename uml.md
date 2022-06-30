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
        + workplace
    }

    TimeInterval --* TimeIntervals 
    Shift ^-- TimeInterval
    Shift *--* Workplace


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
    Worker *--* Shift
    Worker *-- TimeIntervals
    Worker ^-- Person



    class Workplace{
        + timetable
        + workers
    }
    Workplace *-- Worker

    class Qualification{
        + description
    }

    Worker *-- Qualification
    Shift *-- Qualification



@enduml