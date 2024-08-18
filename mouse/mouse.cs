using Godot;
using System;
using System.Collections;

public partial class mouse : CharacterBody2D
{
	
	int speed;
	int base_speed = 125;
	int base_food = 5;
	int food;
	int facing = 1;
	Vector2 direction;

	float animation_scale = 1f;

	AnimationController animation_controller;
	FoodDetector food_detector;
	
	

	void setSize()
	{
		animation_scale = (float)food / base_food;



			if(direction.X > 0)
			{

				food_detector.Scale = new Vector2(1, 1);
				
			}
			if(direction.X < 0)
			{

				food_detector.Scale = new Vector2(-1, 1);
			}

			// GD.Print($"Mouse Scale: {animation_scale}, {Scale}");

			Scale = new Vector2(animation_scale , animation_scale);
			// Scale = new Vector2(Scale.X * facing, Scale.X);
			
	}

	void quickFixScale()
	{
		if (Scale.Y < 0)
		{
			Scale = new Vector2(Scale.X, -Scale.Y);
		}
	}

	public void getEaten()
	{
		food--;
	}

	public void move()
	{
		GD.Print($"Mouse Position: {Position}");
		getDirection();
		Velocity = speed * direction;
		MoveAndSlide();
		
	}


	void eat()
	{
		// GD.Print($"Food Detected: {food_detector.food_detected}");
		if (Input.IsActionJustPressed("eat"))
		{
			animation_controller.eating = true;
			speed = 0;
			
			if (food_detector.food_detected)
			{
				food++;
				GD.Print($"food: {food}");

				foreach(Node2D body in food_detector.overlapping_boddies)
				{
					if (!(body.Name == "Mouse"))
					{
						if(body is base_npc npc)
						{
							npc.getEaten();
						}
						
					}
				}

			}
		}
		else
		{
			animation_controller.eating = false;
			speed = base_speed;
		}

		
		
	}

	public void getDirection()
	{
		int up = Convert.ToInt32(Input.IsActionPressed("up"));
		int down = Convert.ToInt32(Input.IsActionPressed("down"));
		int left = Convert.ToInt32(Input.IsActionPressed("left"));
		int right = Convert.ToInt32(Input.IsActionPressed("right"));

		direction = new Vector2(right - left, down - up).Normalized();
		animation_controller.direction = direction;

	}


	void die()
	{
		if(food <= 0)
		{
			QueueFree();
		}
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{	
		animation_controller = GetNode<AnimationController>("AnimationController");
		food_detector = GetNode<FoodDetector>("FoodDetector");
		food = base_food;
		speed = base_speed;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		move();
		eat();
		setSize();
		quickFixScale();
		die();
		// GD.Print($"{Name}: {Scale}");
	}
}
