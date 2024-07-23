using System.Security.Cryptography.X509Certificates;

namespace ant_colony_optimization;

public class Town
{
    public int X, Y;

    public Town(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}

public class Graph
{
    public List<Town> map;
    public int margin;

    public Graph(int margin)
    {
        this.map = new List<Town>();
        this.margin = margin;
    }

    public void RandTowns(Size boundary, int num_towns)
    {
        Random rng = new Random();
        for (int i = 0; i < num_towns; i++)
        {
            int Rx = rng.Next(margin, boundary.Width) - (margin/2);
            int Ry = rng.Next(margin, boundary.Height) - (margin/2);
            map.Add(new Town(Rx, Ry));
        }
    }

    public void AddTown(int x, int y) {
        map.Add(new Town(x, y));
    }

    public void clear_graph(){
        map.Clear();
    }
}

public class Path {
    public Town start, end;
    public int i_start, i_end;
    public double pheromone;
    public double distance;

    public Path(int i_start, Town start, int i_end, Town end, double pheromone) {
        this.start = start;
        this.i_start = i_start;
        this.end = end;
        this.i_end = i_end;
        this.pheromone = pheromone;
        this.distance = Math.Sqrt( (Math.Abs(start.X - end.X)^2) + (Math.Abs(start.Y - end.Y)^2) );
    }
}

public class Ant {
    public int num_towns, cur_town, start_town;
    public double distance_traveled;
    public List<int> visited;
    public List<List<double>> path_visibilities;
    public List<double> path_posibilities;

    public Ant(int start_town){
        this.path_visibilities = new List<List<double>>();
        this.path_posibilities = new List<double>();
        this.distance_traveled = 0;
        this.visited = new List<int>();
        this.cur_town = start_town;
        this.start_town = start_town;
    }

    public void calc_initial_path_vis(List<List<Path>> path_dist_matrix) {
        this.num_towns = path_dist_matrix.Count;
        for(int a = 0; a < this.num_towns; a++){
            List<double> a_vis = new List<double>();
            for(int b = 0; b < this.num_towns; b++){
                double inv_dist = path_dist_matrix[a][b].distance == 0 ? 0 : (1 / path_dist_matrix[a][b].distance);
                a_vis.Add( inv_dist ); //assign inverse of distance between towns 'a' and 'b'
            }
            path_visibilities.Add(a_vis);
        }
    }

    public void visit_town(int x){
        this.visited.Add(x);
        //mark path to town 'x' as a zero, already visited, cannot visit again
        for (int a = 0; a < this.num_towns; a++){
            this.path_visibilities[a][x] = 0;
        }
    }

    public void calc_new_visit_possibilities(List<List<double>> pheremones, double alpha, double beta) {
        //calculate a list of possibilities to visit each other city from 'cur_town'
        path_posibilities = new List<double>();
        double running_total = 0;
        for(int a = 0; a < pheremones.Count; a++){
            double prob = Math.Pow(pheremones[cur_town][a],alpha) * Math.Pow(path_visibilities[cur_town][a],beta);
            path_posibilities.Add( prob );
            running_total += prob;
        }
        for(int b = 0; b < path_posibilities.Count; b++){
            path_posibilities[b] = path_posibilities[b] / running_total;
        }
    }

    public int choose_next_town(){
        //choose the next town to vist from 'cur_town'
        List<double> cumulative_probs = new List<double>();
        double running_sum = 0;
        //develop a list of probabilities to visit each town. from town 0 to n in order
        for(int i = 0; i < path_posibilities.Count; i++){
            running_sum += path_posibilities[i];
            cumulative_probs.Add(running_sum);
        }
        Random rand = new Random();
        double r = rand.NextDouble();
        for(int j = 0; j < cumulative_probs.Count; j++){
            if(r < cumulative_probs[j]){
                return j;
            }
        }
        return this.start_town;
    }

    public void tour_the_world(List<List<Path>> path_dist_matrix, List<List<double>> pheremones, double alpha, double beta){
        calc_initial_path_vis(path_dist_matrix);
        while(visited.Count <= this.num_towns){
            visit_town(cur_town);
            calc_new_visit_possibilities(pheremones, alpha, beta);
            int next_town = choose_next_town();
            distance_traveled += path_dist_matrix[cur_town][next_town].distance;
            cur_town = next_town;
        }
    }
}

public class AntColonyPathOptimization {
    private int num_towns;
    private List<List<Path>> path_dist_matrix;
    private List<List<double>> pheremones;
    private int num_ants = 10;
    private int num_iterations = 200;
    private double evaporation_rate = 0.5;
    private double alpha = 1.8; //pheremone importance
    private double beta = 2.0; //visibility importance

    public AntColonyPathOptimization(Graph world){
        this.num_towns = world.map.Count;
        this.pheremones = new List<List<double>>();
        this.path_dist_matrix = new List<List<Path>>();
        for (int a = 0; a < num_towns; a++){
            List<Path> paths = new List<Path>();
            List<double> pher = new List<double>();
            for (int b = 0; b < num_towns; b++){
                //compile a list of paths to from town 'a' to every other town to town 'b' (even to itself)
                paths.Add(new Path(a, world.map[a], b, world.map[b], 1.0));
                pher.Add(1.0);
            }
            path_dist_matrix.Add(paths);
            pheremones.Add(pher);
        }
    }

    public List<Ant> ExploreWorld()
    {
        Random rand = new Random();
        //birth the ants, starting each in a random location
        List<Ant> ants = new List<Ant>();
        for (int ant = 0; ant < num_ants; ant++)
        {
            int start_town = rand.Next(num_towns);
            ants.Add(new Ant(start_town));
        }
        //now have each ant make a full tour of every town
        foreach (Ant ant in ants)
        {
            ant.tour_the_world(path_dist_matrix, pheremones, alpha, beta);
        }
        return ants;
    }

    private void UpdatePheremones(List<Ant> ants)
    {
        //update pheremones on all paths
        //evaporation first
        for (int a = 0; a < num_towns; a++)
        {
            for (int b = 0; b < num_towns; b++)
            {
                pheremones[a][b] = pheremones[a][b] * (1 - evaporation_rate);
                //add pheremones laid by the ants
                foreach (Ant ant in ants)
                {
                    pheremones[a][b] = pheremones[a][b] + (1 / ant.distance_traveled);
                }
            }
        }
    }

    public List<int> GetOptimalPath(){
        List<Ant> ants = new List<Ant>();
        for(int a = 0; a < num_iterations; a++){
            ants = ExploreWorld();
            UpdatePheremones(ants);
        }
        double shortest_path = ants[0].distance_traveled;
        int ant_index = 0;
        for(int i = 0; i < ants.Count; i++){
            if(ants[i].distance_traveled < shortest_path){
                shortest_path = ants[i].distance_traveled;
                ant_index = i;
            }
        }
        return ants[ant_index].visited;
    }
}