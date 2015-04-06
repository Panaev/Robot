using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Robot
{
    public class Init
    {
        public static Graph GraphInit()//сделал инициализацию в отдельном файле и методе, а то больно она громоздкая
        {
            var graph = Graph.MakeGraph(
                0, 6,
                6, 7,
                7, 8,
                8, 2,
                2, 1,
                2, 3,
                3, 4,
                3, 9,
                9, 10,
                10, 11,
                11, 5,
                8, 14,
                14, 13,
                13, 12,
                14, 15,
                9, 15,
                15, 16,
                16, 17,
                12, 18,
                18, 19,
                19, 20,
                20, 21,
                21, 22,
                22, 23,
                17, 23);
            
            graph.nodes[0].Y = graph.nodes[1].Y = graph.nodes[2].Y = graph.nodes[3].Y = graph.nodes[4].Y = graph.nodes[5].Y = 75;
            graph.nodes[6].Y = graph.nodes[7].Y = graph.nodes[8].Y = graph.nodes[9].Y = graph.nodes[10].Y = graph.nodes[11].Y = 25;
            graph.nodes[12].Y = graph.nodes[13].Y = graph.nodes[14].Y = graph.nodes[15].Y = graph.nodes[16].Y = graph.nodes[17].Y = -25;
            graph.nodes[18].Y = graph.nodes[19].Y = graph.nodes[20].Y = graph.nodes[21].Y = graph.nodes[22].Y = graph.nodes[23].Y = -75;

            graph.nodes[0].X = graph.nodes[6].X = graph.nodes[12].X = graph.nodes[18].X = -125;
            graph.nodes[1].X = graph.nodes[7].X = graph.nodes[13].X = graph.nodes[19].X = -75;
            graph.nodes[2].X = graph.nodes[8].X = graph.nodes[14].X = graph.nodes[20].X = -25;
            graph.nodes[3].X = graph.nodes[9].X = graph.nodes[15].X = graph.nodes[21].X = 25;
            graph.nodes[4].X = graph.nodes[10].X = graph.nodes[16].X = graph.nodes[22].X = 75;
            graph.nodes[5].X = graph.nodes[11].X = graph.nodes[17].X = graph.nodes[23].X = 125;

            return graph;
        }
    }
}
