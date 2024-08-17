using Godot;
using System;

public partial class base_npc : StaticBody2D
{
	[Export]
	int speed = 100;
	int scale = 1;
	int food;

	[Export]
	int base_food = 5;
	AnimationController animation_controller;
	Vector2 Velocity;
	Vector2 target;
	
	void setSize()
	{
		scale = food / base_food;
	}

	void MoveAndSlide()
	{
		Position = Position + Velocity;
	}

	public void move(double delta)
	{
		Vector2 direction = getDirection();
		Velocity = speed * direction;
		Translate(Velocity * (float)delta);
	}

	public virtual Vector2 getDirection()
	{
		//direction will come from ai behavior
		target = Position;
		Vector2 direction = (target - Position).Normalized();
		return direction;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		animation_controller = GD.Load<AnimationController>("AnimationController");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		move(delta);

	}
}
