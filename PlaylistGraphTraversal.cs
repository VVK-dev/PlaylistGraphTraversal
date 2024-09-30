using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeWorldCSharp
{
    internal class PlaylistGraphTraversal
    {

        //When looked at graphically, this problem forms a sort of tree structure that can be traversed
         
        private static int getshortestpath(int p, int n, int k, Span<int> parents, Span<int> children)
        {

            /* parents = array of time it takes to watch videos for the first time, children = array of time it takes to 
             * rewatch corresponding parent video
             * 
             * p = index of current parent node, n = number of nodes traversed so far, k = number of nodes to 
             * traverse in total, t = number of nodes left to traverse 
             */

            int path = parents[n]; n++; p++; //first step in finding shortest path is to traverse parent node

            if (n == k) { return path; }

            int t = k - n;

            int[] possiblepaths = new int[t+2]; //2 extra slots for 2 extra paths recursion doesn't traverse

            //First possible path is the child only path

            possiblepaths[0] = children[p] * t;

            for (int i = 1; i <= t; i++)
            {

                possiblepaths[i] = children[n] * i; //traverse child node i times

                int j = n + i; //number of nodes traversed after traversing child node

                //After traversing the child node, only traverse through next nodes if n + i !=k

                possiblepaths[i] += (j == k) ? 0 : getshortestpath(p, j, k, parents, children);

                //Last possible path is parents-only path

                possiblepaths[^1] += parents[p+i];

            }

            path += possiblepaths.Min();

            return path;

        }


        public static int getleastwatchtime(int k, int[] playlist, int[] repeatwatchtime)
        {

            //given constraint in problem: first time watching a video is watch time + repeat watch time

            int[] firstwatch = playlist.Zip(repeatwatchtime, (p, r) => p + r).ToArray();

            return getshortestpath(-1, 0, k, firstwatch.AsSpan(), repeatwatchtime.AsSpan());
    
        }



        public static void Main()
        {


            int[] playlist = { 1, 2, 3, 4, 5, 6 };
            int[] repeatwatch = { 1, 2, 3, 4, 5, 6 };

            int k = 4;

            Console.WriteLine(getleastwatchtime (k, playlist, repeatwatch));


        }

    }
}
