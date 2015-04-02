namespace RobotDevelopment
{
	class Robot
	{
		//тут я предлагаю писать робота О_О
	}
	
	public class Item //донт рипит йорселф говорите? :)
    {
        public double X;
        public double Y;
        public string type;
        public string orientation = null;
        public string color;
        public Item(MapItem item)
        {
            X = item.X;
            Y = item.Y;
            if (item.Tag == "BlueDetail" || item.Tag == "RedDetail" || item.Tag == "GreenDetail")
            {
                type = "Detail";
                orientation = null;
                if (item.Tag == "BlueDetail")
                    color = "Blue";
                if (item.Tag == "RedDetail")
                    color = "Red";
                if (item.Tag == "GreenDetail")
                    color = "Green";
            }
            else
            {
                type = "Tube";
                if (item.Tag == "VerticalWall" || item.Tag == "VerticalBlueSocket" || item.Tag == "VerticalRedSocket" || item.Tag == "VerticalGreenSocket")
                {
                    orientation = "Vertical";
                    if (item.Tag == "VerticalWall")
                        color = "Grey";
                    if (item.Tag == "VerticalBlueSocket")
                        color = "Blue";
                    if (item.Tag == "VerticalRedSocket")
                        color = "Red";
                    if (item.Tag == "VerticalGreenSocket")
                        color = "Green";
                }
                else
                {
                    orientation = "Horizontal";
                    if (item.Tag == "HorizontalWall")
                        color = "Grey";
                    if (item.Tag == "HorizontalBlueSocket")
                        color = "Blue";
                    if (item.Tag == "HorizontalRedSocket")
                        color = "Red";
                    if (item.Tag == "HorizontalGreenSocket")
                        color = "Green";
                }

            }
        }
    }
	
	public void CreateGraph()
	{
		
	}
}
