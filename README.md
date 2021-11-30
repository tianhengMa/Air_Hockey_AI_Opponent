# Air Hockey With Deep Reinforcement Learning

## Summary:
This project is a **single-player Air Hockey game**. The human player (*red*) can play against an AI opponent (*blue*). I trained the AI model using **Deep Reinforcement Learning** with Unity's [ML-Agent](https://github.com/Unity-Technologies/ml-agents/blob/release_18_docs/docs/Readme.md) package. Through a process called **self-play**, the model is able to learn how to play air hockey from scratch by playing it with itself. 

In *AI vs Human* mode, user can control the red player's position by dragging the mouse while pressing the left mouse key. Try to hit the puck into the opponent's goal to score. In *AI vs AI* mode, user can watch how two AI players play against each other. 

## Demo:
https://user-images.githubusercontent.com/37155540/143982214-270d0b4f-122f-41d7-8569-2f1c331391ae.mp4

## Training:
Training the model with ML-Agent inside ml-agent directory: mlagents-learn --force config/ppo/AirHockey.yaml --run-id=AirHockey
Using Tensor Board to see training progress: tensorboard --logdir results --port 6006 
