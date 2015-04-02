namespace RobotDevelopment
{
  class Edge //ребро
  {
    public readonly Node From;
	  public readonly Node To;
	  
	  public Edge(Node first, Node second)
	  {
		  this.From = first;
		  this.To = second;
	  }
	  
	  public bool IsIncident(Node node)
	  {
  		return From == node || To == node;
	  }
	  
	  public Node OtherNode(Node node)
	  {
		  if (From == node) return To;
		  return From;
	  }
  }
}
