using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 50000;
	
	[Export]
	public float Gravity { get; set; } = 1000;
		
	[Export]
	public float JumpStrength { get; set; } = 1000;
	
	private AnimatedSprite2D _sprite;
	
	public override void _Ready()
	{
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_sprite.Play("idle");
	}

	public override void _Process(double delta)
	{
		var velocity = Velocity;
		
		if (Input.IsActionPressed("Left") && !Input.IsActionPressed("Right"))
		{
			velocity.X = -Speed * (float)delta;
		}
		else if (Input.IsActionPressed("Right") && !Input.IsActionPressed("Left"))
		{
			velocity.X = Speed * (float)delta;
		}
		else
		{
			velocity.X = 0;
		}
		
		if (!IsOnFloor())
		{
			velocity.Y += Gravity * (float)delta;
		}
		else if (Input.IsActionJustPressed("Jump"))
		{
			velocity.Y = -JumpStrength;
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
