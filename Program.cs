namespace RobotDevelopment
{
  public class Program
  {
  	public static void Main()
  	{
    		var server = new CvarcClient(args, Settings).GetServer<PositionSensorsData>();
		var helloPackageAns = server.Run();
    		var mainloop = true;
    		Robot robot = new Robot();
    		while (mainloop)// робот работает в "вечном" цикле
    		{
      			Graph map = new Graph();// создаётся карта - каждый раз по новой, запихивается в граф
      			// затем находим деталь, которая находится ближе всего
      			// и формируем alpha - угол, на который ему следует повернуть
      			// З.Ы. надо обходить препятствия...
      			// З.З.Ы. по идее, надо делать это эффективно...
      			double alpha = ...;
      			robot.Rotate(alpha);
      			robot.Go();
      			// mainloop становится false, когда на карте нет необходимого объекта
    		}
    		server.Exit()
  	}
  	
  	public static void Go()
  	{
  		sensorsData = server.SendCommand(new Command { LinearVelocity = 50, Time = 1 });
  	}
  	
  	public static Rotate(angle)
  	{
  		sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(angle), Time = 1 });
  	}
  }
}
