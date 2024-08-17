using Godot;
using System;
using System.Diagnostics.Tracing;

public partial class base_npc : StaticBody2D
{
	[Export]
	int speed = 100;
	float scale = 1;

	int food;

	[Export]
	int base_food = 5;
	AnimationController animation_controller;
	Vector2 Velocity;
	Vector2 target;
	
	void setSize()
	{
		scale = (float)food / base_food;
		Vector2 one_vector = new Vector2(1f,1f);
		animation_controller.Scale = one_vector * scale;
	}

	public virtual void Eat()
	{

	}

	public virtual void setTarget()
	{
		
	}


	void die()
	{
		if (food == 0)
		{
			QueueFree();
		}
	}

	public void getEaten()
	{
		GD.Print("Getting Eaten");
		GD.Print($"food: {food}, scale: {Scale}");
		food--;
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
		food = base_food;
		animation_controller = GetNode<AnimationController>("AnimationController");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		setSize();
		move(delta);
		die();

	}

}
