# Reward Machine Agent for Patrolling
> A patrolling agent controlled by a MCTS planner using a Reward Machine, capable of dealing with non-deterministic observations.

## Description
A framework for controlling agents in Unity (3D real-time engine). The algorithm in the framework is based on my work with Reward Machines: Instead of rewarding an agent for a given action in a given state, a reward machine allows one to specify rewards for sequences of observations. Every observation is mapped from an action-state pair. For instance, if you want to make your agent kick the ball twice in a row, then give it a reward only after seeing that it has kicked the ball twice in a row. A regular reward function would only be able to give the same reward for the first and second kick.

I implemented a Monte Carlo Tree Search (MCTS) planner, which plans over the given reward machine. In this patrolling environment, the observation mapping function is not completely deterministic. This means that the MCTS planner must be based on a partially observable Markov decision process (POMDP). The project (repository) for a *treasure-hunting* reward machine agent has only deterministic observations. The treasure-hunting environment thus makes use of a MCTS planner based on a (fully observable) Markov decision process (MDP).

## Installation and/or Setup
To be provided.
The project can be cloned from [GitHub](https://github.com/GavinRens/Reward-Machine-Agent---Patrolling)

## Usage / API Reference
 - To be provided.

## Options

### Parameters
- To be provided.
- To be provided.

### Environment design
- To be provided.

## References
- To be provided.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)