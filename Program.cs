namespace RobotDevelopment
{
  public static class Program
  {
    var mainloop = True;
    Robot robot = new Robot();
    while (mainloop)// робот работает в "вечном" цикле
    {
      // создаётся карта - каждый раз по новой, запихивается в граф
      robot.GoTo(map);
    }
  }
}
