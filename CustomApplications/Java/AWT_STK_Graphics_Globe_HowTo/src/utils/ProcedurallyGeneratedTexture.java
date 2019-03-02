package utils;

public class ProcedurallyGeneratedTexture
{
	public ProcedurallyGeneratedTexture()
	{
		initialize(256, 0x0000FF);
	}

	public ProcedurallyGeneratedTexture(int size)
	{
		initialize(size, 0x0000FF);
	}

	public ProcedurallyGeneratedTexture(int size, int rgb)
	{
		initialize(size, rgb);
	}

	final private void initialize(int size, int rgb)
	{
		m_x = 0;
		m_size = size;
		m_r = rgb & 0xFF0000;
		m_g = rgb & 0x00FF00;
		m_b = rgb & 0x0000FF;
		m_a = 255;
		genTexture();
	}

	static private double noise(int x, int y)
	{
		int n = x + y * 57;
		n = (n << 13) ^ n;
		return (1.0 - ((n * (n * n * 19973 + 97003) + 104729) & 0x7FFFFFFF) / 1073741824.0);
	}

	static private double interpolate(double a, double b, double x)
	{
		double f = (1 - Math.cos(x * Math.PI)) * 0.5;
		return a * (1 - f) + b * f;
	}

	static private double smoothNoise(int x, int y)
	{
		double corners = (noise(x - 1, y - 1) + noise(x + 1, y - 1) + noise(x - 1, y + 1) + noise(x + 1, y + 1)) / 16;
		double sides = (noise(x - 1, y) + noise(x + 1, y) + noise(x, y - 1) + noise(x, y + 1)) / 8;
		double center = noise(x, y) / 4;
		return corners + sides + center;
	}

	static private double interpolateNoise(double x, double y)
	{
		int iX = (int)x;
		double dX = x - iX;
		int iY = (int)y;
		double dY = y - iY;
		double v1 = smoothNoise(iX, iY);
		double v2 = smoothNoise(iX + 1, iY);
		double v3 = smoothNoise(iX, iY + 1);
		double v4 = smoothNoise(iX + 1, iY + 1);
		double i1 = interpolate(v1, v2, dX);
		double i2 = interpolate(v3, v4, dX);
		return interpolate(i1, i2, dY);
	}

	static private double genNoise(double x, double y, double size)
	{
		double total = 0.0;
		double initialSize = size;
		for(int i = 0; i < size; ++i)
		{
			total += interpolateNoise(x / size, y / size) * size;
			size /= 2.0;
		}
		return total / initialSize;
	}

	final private void genTexture()
	{
		m_texture = new byte[m_size * m_size * 4];
		for(int i = 0; i < m_size; ++i)
		{
			for(int j = 0; j < (m_size * 4) - 4; j += 4)
			{
				double color = genNoise(i, j / 4, 64D) * 255;
				int index = i * m_size * 4 + j;
				m_texture[index] = (byte)((m_r / 255.0) * color);
				m_texture[index + 1] = (byte)((m_g / 255.0) * color);
				m_texture[index + 2] = (byte)((m_b / 255.0) * color);
				m_texture[index + 3] = (byte)m_a;
			}
		}
		m_x = m_size;
	}

	public final byte[] next()
	{
		byte[] texture = new byte[m_size * m_size * 4];
		for(int i = m_size * 4; i < m_size * m_size * 4; ++i)
		{
			texture[i - m_size * 4] = m_texture[i];
		}
		int index = (m_size * m_size * 4) - (m_size * 4);
		for(int j = 0; j < m_size * 4; j += 4)
		{
			double color = genNoise(m_x, j / 4, 64D) * 255;
			texture[index + j] = (byte)((m_r / 255.0) * color);
			texture[index + j + 1] = (byte)((m_g / 255.0) * color);
			texture[index + j + 2] = (byte)((m_b / 255.0) * color);
			texture[index + j + 3] = (byte)m_a;
		}
		m_x++;
		m_texture = texture;
		return m_texture;
	}

	private int		m_x;
	private int		m_size;
	private byte[]	m_texture;
	private int		m_r;
	private int		m_g;
	private int		m_b;
	private int		m_a;
}