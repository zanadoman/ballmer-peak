using Godot;
using System;

public partial class Platform : Node2D
{
	private float Offset;
	private float Direction = 1;
	
	static Random random = new();
		
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Direction = random.Next(0, 2) == 1 ? -1 : 1;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Offset += Direction * 200 *  (float)delta;
		Position = new Vector2(Position.X + Direction * 200 * (float)delta, Position.Y);
		if (Offset < -400)
		{
			Direction = 1;
		}
		else if (400 < Offset)
		{
			Direction = -1;
		}
	}
}
