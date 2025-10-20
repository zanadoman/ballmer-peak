using Godot;
using System;

internal enum Facing
{
	Left,
	Right
}

public partial class Player : CharacterBody2D
{
	[Export]
	public float Speed { get; set; } = 50000;
	
	[Export]
	public float Gravity { get; set; } = 5000;
		
	[Export]
	public float JumpStrength { get; set; } = 2500;
	
	private Facing _facing = Facing.Right;
	
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
			_facing = Facing.Left;
		}
		else if (Input.IsActionPressed("Right") && !Input.IsActionPressed("Left"))
		{
			velocity.X = Speed * (float)delta;
			_facing = Facing.Right;
		}
		else
		{
			velocity.X = 0;
		}
		
		_sprite.FlipH = _facing == Facing.Left;
		
		if (!IsOnFloor())
		{
			velocity.Y += Gravity * (float)delta;
			_sprite.Play(velocity.Y < 0 ? "dump" : "fall");
		}
		else if (Input.IsActionJustPressed("Jump"))
		{
			velocity.Y = -JumpStrength;
			_sprite.Play("jump");
		}
		else
		{
			_sprite.Play(velocity.X == 0 ? "idle" : "run");
		}
		
		Velocity = velocity;
		MoveAndSlide();
	}
}
