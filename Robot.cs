namespace RobotDevelopment
{
  public class Robot
  {
    public Pos { get { return Position; } } // как это пишется, поправьте меня
    public DetailsInfo { get { return DetailsInfo; } }
    
    public GoBy(double deltaX, double deltaY)
    {
      // робот изменяет свою координату
    }
    
    public Rotate(double alpha) // или он не принимает угол...
    {
      // поворот робота
    }
  }
}
