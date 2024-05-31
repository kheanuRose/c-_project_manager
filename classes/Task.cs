//task class with the propperties for each task
abstract class Task{
    public string Title;
    public string Description;
    public string Due_date;
    public int Priority;
    public string Status;
    public Task(string Title, string Description, string Due_date, int Priority,string Status){
        this.Title = Title;
        this.Description = Description;
        this.Due_date = Due_date;
        this.Priority = Priority;
        this.Status = Status;
    }
}