namespace RobotDevelopment
{
  public class Program
  {
  	public static void Main()
  	{
    		var server = new CvarcClient(args, Settings).GetServer<PositionSensorsData>();
		var helloPackageAns = server.Run();
		sensorsData = server.SendCommand(new Command { LinearVelocity = 10, Time = 1 });
		sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(-90), Time = 1 });
		sensorsData = server.SendCommand(new Command { LinearVelocity = 10, Time = 1 });
    		var mainloop = true;
    		while (mainloop)// робот работает в "вечном" цикле
    		{
      			Graph map = new Graph();// создаётся карта - каждый раз по новой, запихивается в граф
      			// затем находим деталь, которая находится ближе всего
      			// и формируем alpha - угол, на который ему следует повернуть
      			// З.Ы. надо обходить препятствия...
      			// З.З.Ы. по идее, надо делать это эффективно...
      			double alpha = ...;
      			Rotate(angle, server);
      			Go(server)
      			// mainloop становится false, когда на карте нет необходимого объекта
    		}
    		server.Exit()
  	}
  	
  	public static void Go(server)
  	{
  		sensorsData = server.SendCommand(new Command { LinearVelocity = 50, Time = 1 });
  	}
  	
  	public static Rotate(angle, server)
  	{
  		sensorsData = server.SendCommand(new Command { AngularVelocity = Angle.FromGrad(angle), Time = 1 });
  	}
  }
}
