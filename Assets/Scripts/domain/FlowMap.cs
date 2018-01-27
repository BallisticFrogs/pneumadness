namespace domain
{
    public class FlowMap
    {
        public readonly int employee;
        public readonly int[][] grid;

        public FlowMap(int employee, int width, int height)
        {
            this.employee = employee;
            grid = new int[width][];
            for (int i = 0; i < width; i++)
            {
                grid[i] = new int[height];
            }
        }
    }
}