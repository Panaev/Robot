namespace RobotDevelopment
{
  public static class Program
  {
    var mainloop = True;
    Robot robot = new Robot();
    while (mainloop)// робот работает в "вечном" цикле
    {
      Graph map = new Graph();// создаётся карта - каждый раз по новой, запихивается в граф
      // затем находим деталь, которая находится ближе всего
      // и формируем deltaX, deltaY и alpha - необходимое изменение координаты робота
      // и угол, на который ему следует повернуть
      // З.Ы. надо обходить препятствия...
      // З.З.Ы. по идее, надо делать это эффективно...
      double x = ...;
      double y = ...;
      double alpha = ...;
      robot.GoTo(x, y);
      robot.Rotate(alpha);
    }
  }
}
