using Godot;
using System;

public partial class mouse : CharacterBody2D
{
	
	int speed = 75;

	AnimationController animation_controller;
	

	public void move()
	{
		Vector2 direction = getDirection();
		Velocity = speed * direction;
		MoveAndSlide();
		
	}

	void eat()
	{
		if (Input.IsActionPressed("eat"))
		{
			animation_controller.eating = true;
			speed = 0;
		}
		else
		{
			animation_controller.eating = false;
			speed = 75;
		}
	}

	public Vector2 getDirection()
	{
		int up = Convert.ToInt32(Input.IsActionPressed("up"));
		int down = Convert.ToInt32(Input.IsActionPressed("down"));
		int left = Convert.ToInt32(Input.IsActionPressed("left"));
		int right = Convert.ToInt32(Input.IsActionPressed("right"));

		Vector2 direction = new Vector2(right - left, down - up).Normalized();
		animation_controller.direction = direction;

		return direction;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		animation_controller = GetNode<AnimationController>("AnimationController");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		move();
		eat();
	}
}
