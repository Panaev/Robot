namespace RobotDevelopment
{
  public class Robot
  {
    public Pos { get { return Position; } } // как это пишется, поправьте меня
    public DetailsInfo { get { return DetailsInfo; } } // и это тоже...
    public Direction { get { return ... } } // направление
    
    public Go()
    {
      // робот едет
      PositionSensorsData sensorsData = null;
      sensorsData = server.SendCommand(new Command { LinearVelocity = 50, Time = 1 });
    }
    
    public Rotate(double alpha) // или он принимает не угол...
    {
      // поворот робота
      PositionSensorsData sensorsData = null;
      sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(alpha), Time = 1 });
    }
  }
}
