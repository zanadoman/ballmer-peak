using Godot;
using System;

public partial class LevelGen : Node2D
{
	[Export] public PackedScene PlatformScene { get; set; }
	[Export] public int PlatformCount { get; set; } = 30;
	[Export] public float MinX { get; set; } = -400f;
	[Export] public float MaxX { get; set; } = 400f;
	[Export] public float StartY { get; set; } = 0f;
	[Export] public float GapMin { get; set; } = 40f;
	[Export] public float GapMax { get; set; } = 140f;
	[Export] public Vector2 PlatformWidthRange { get; set; } = new Vector2(100f, 300f);
	[Export] public int Seed { get; set; } = 0;
	
	 private RandomNumberGenerator rng = new RandomNumberGenerator();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Generate();
	}
	
	public void Generate(){
		if (PlatformScene == null){
			GD.PrintErr("PlatformScene nincs beállítva!");
			return;
		}
		
		 foreach (Node child in GetChildren())
		{
			// ha vannak speciális childok, filtereld őket ki itt
			if (child.Name == "Player") // pl ha van Player, ne töröld
				continue;
			child.QueueFree();
		}
		
		 if (Seed != 0)
			rng.Seed = (ulong)Seed;
		else
			rng.Randomize();
			
		 float y = StartY;
		
		for (int i = 0; i < PlatformCount; i++)
		{
			float width = rng.RandfRange(PlatformWidthRange.X, PlatformWidthRange.Y);
			float x = rng.RandfRange(MinX, MaxX);

			Node2D inst = PlatformScene.Instantiate() as Node2D;
			if (inst == null){
				continue;
			}
			
			 inst.Position = new Vector2(x, y);
			
			var col = inst.GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
			if (col != null && col.Shape is RectangleShape2D rectShape)
			{
				rectShape.Size = new Vector2(width, rectShape.Size.Y);
			}
			else
			{
				var colAny = inst.GetNodeOrNull<CollisionShape2D>("CollisionShape2D");
				if (colAny != null && colAny.Shape is RectangleShape2D r2)
					r2.Size = new Vector2(width * 0.5f, r2.Size.Y);
			}
			
			var sprite = inst.GetNodeOrNull<Sprite2D>("Sprite2D");
			if (sprite != null && sprite.Texture != null)
			{
				Vector2 texSize = new Vector2(sprite.Texture.GetWidth(), sprite.Texture.GetHeight());
				if (texSize.X > 0)
				{
					float scaleX = width / texSize.X;
					sprite.Scale = new Vector2(scaleX, 1f);
				}
			}
			
			AddChild(inst);
			y -= rng.RandfRange(GapMin, GapMax);
			
			GD.Print($"Generated {PlatformCount} platforms.");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
