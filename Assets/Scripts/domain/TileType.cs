public enum TileType
{
    OFFICE = 0,
    PIPE_STRAIGHT_VERT = Dir.UP | Dir.DOWN,
    PIPE_STRAIGHT_HORIZ = Dir.LEFT | Dir.RIGHT,
    PIPE_BEND_UR = Dir.UP | Dir.RIGHT,
    PIPE_BEND_UL = Dir.UP | Dir.LEFT,
    PIPE_BEND_DR = Dir.DOWN | Dir.RIGHT,
    PIPE_BEND_DL = Dir.DOWN | Dir.LEFT,
    PIPE_T_L = Dir.DOWN | Dir.UP | Dir.LEFT,
    PIPE_T_R = Dir.DOWN | Dir.UP | Dir.RIGHT,
    PIPE_T_U = Dir.UP | Dir.RIGHT | Dir.LEFT,
    PIPE_T_D = Dir.DOWN | Dir.RIGHT | Dir.LEFT,
    PIPE_BEND_CROSS = Dir.UP | Dir.RIGHT | Dir.LEFT | Dir.DOWN
}

public static class TileTypeMethods
{
    public static bool IsConnectedTo(this TileType type, Dir dir)
    {
        return ((int) type & (int) dir) > 0;
    }
}

public enum Dir
{
    UP = 1,
    DOWN = 2,
    RIGHT = 4,
    LEFT = 8
}