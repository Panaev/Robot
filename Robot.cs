namespace RobotDevelopment
{
  public class Robot
  {
    public Pos { get { return Position; } } // как это пишется, поправьте меня
    public DetailsInfo { get { return DetailsInfo; } }
    
    public Go()
    {
      // робот едет
      sensorsData = server.SendCommand(new Command { LinearVelocity = 50, Time = 1 });
    }
    
    public Rotate(double alpha) // или он принимает не угол...
    {
      // поворот робота
      sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(alpha), Time = 1 });
    }
  }
}
