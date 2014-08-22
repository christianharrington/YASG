using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using System.Diagnostics;

public class Ship : ITurnable, IVehicle {
	private ILocation location;
	private readonly HashSet<ILocation> sublocations = new HashSet<ILocation>();
    private ILocation destination, previousDestination;
    private Vector2 localCoordinates;
    private readonly string name;
    private readonly System.Random random;

    private readonly Universe universe;

	public readonly float MaxSpeed = 1.0f;
    public readonly float MaxAccel = 0.0001f; // light years / year
    private Vector2 velocity = new Vector2(0f, 0f);

    public Ship(System.Random random, ILocation location, Vector2 localCoordinates) {
        this.random = random;
        this.location = location;
        this.localCoordinates = localCoordinates;

        this.name = shipNameGenerator();

		//this.Destination = new Destination(location, localCoordinates);

        ILocation parent = location;

        while (universe == null) {
            if (parent is Universe) {
                universe = parent as Universe;
            }
            else {
                parent = parent.Location;
            }
        }
    }

    private string shipNameGenerator() {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var result = new string(
            Enumerable.Repeat(chars, 2)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());

        return "USS-" + result + random.Next(100, 999);
    }

	public ILocation Destination {
        get {
            return destination;
        }
        set {
            previousDestination = destination;
			destination = value;
            UnityEngine.Debug.Log("Destination coords: " + destination.Coordinates);
        }
    }

    private ILocation findLocation(HashSet<ILocation> candidates, ILocation bestCandidate) {
        HashSet<ILocation> newCandidates = new HashSet<ILocation>();

        foreach (ILocation c in candidates) {
            double dist = Vector2.Distance(Coordinates, c.Coordinates);
 
            if (dist <= c.AreaOfInfluence) {

                newCandidates.UnionWith(c.Sublocations);

                if (bestCandidate != null && dist < Vector2.Distance(Coordinates, bestCandidate.Coordinates)) {
                    bestCandidate = c;
                }
                else {
                    bestCandidate = c;
                }
            }
        }

        if (newCandidates.Count == 0) {
            if (bestCandidate != null) {
                return bestCandidate;
            }
            else {
                return universe;
            }
        }
        else {
            return findLocation(newCandidates, bestCandidate);
        }
    }

	public ILocation Location {
		get {
			return findLocation(universe.Sublocations, null);
		}
	}

	public HashSet<ILocation> Sublocations {
		get {
			return sublocations;
		}
	}

    public Vector2 LocalCoordinates {
        get {
            return localCoordinates;
        }
    }

    public Vector2 Coordinates {
        get {
            return location.LocalCoordinates + localCoordinates;
        }
    }
	
    public string Name {
        get {
            return name;
        }
    }

//	public void Turn (double turnTime, double targetDate) {
//        Debug.Log("Location of Ship: " + Location);
//
//        if (Destination != null && Coordinates != Destination.Coordinates) {
//            Vector2 movement = Vector2.MoveTowards(Coordinates, Destination.Coordinates, MaxSpeed * Convert.ToSingle(turnTime)); // FIXME: This is wrong
//            Debug.Log(movement);
//            localCoordinates = movement - location.LocalCoordinates;
//        }

//	private void decelerate() {
//		speed = speed > MaxAccel ? speed - MaxAccel : 0.0f;
//	}
//
//	private void accelerate() {
//		speed = speed < MaxSpeed && speed + MaxAccel < MaxSpeed ? speed + MaxAccel : MaxSpeed;
//	}
//
//	private float yearsRequiredToReachSpeed(float targetSpeed) {
//		return Math.Abs ((targetSpeed - speed) / MaxAccel);
//	}
//
//	private void moveTowards(Vector2 destinationCoordinates, float maxDistance) {
//		Vector2 movement = Vector2.MoveTowards(Coordinates, destinationCoordinates, maxDistance);
//		UnityEngine.Debug.Log("Movement: " + movement);
//		localCoordinates = movement - location.LocalCoordinates;
//	}
//
//	private Vector2 simulateMove(Vector2 destinationCoordinates, float maxDistance) {
//		Vector2 movement = Vector2.MoveTowards(Coordinates, destinationCoordinates, maxDistance);
//		return location.LocalCoordinates + movement - location.LocalCoordinates;
//	}
//
//	private float distanceTravelled(float initialSpeed, float endSpeed) {
//		return Math.Abs ((initialSpeed - endSpeed) / 2);
//	}
//
//	private float distanceToDestination(Vector2 coordinates) {
//		return Vector2.Distance(coordinates, Destination.Coordinates);
//	}
//
//	private bool isOvershooting(Vector2 currentLocation) {
//		return distanceToDestination(currentLocation) > yearsRequiredToReachSpeed(0.0f);
//	}

	private enum MoveState {
		ACCELERATING, CRUISING, DECELERATING, STOPPED
	}

	private double distanceToDestination() {
		return Vector2.Distance(Coordinates, Destination.Coordinates);
	}

	private double distanceToDestination(Vector2 position) {
		return Vector2.Distance(position, Destination.Coordinates);
	}

	private double timeToDestination() {
		return distanceToDestination() / velocity.magnitude;
	}

	private double timeToDestination(Vector2 position) {
		return distanceToDestination(position) / velocity.magnitude;
	}

	private MoveState nextState() {
		switch (currentMoveState) {
		case MoveState.CRUISING:
			System.Diagnostics.Debug.Assert(velocity.magnitude >= MaxSpeed);

			Vector2 newPosition = velocity + localCoordinates;
			double timeToDecelerate = velocity.magnitude / MaxAccel;

			if (timeToDecelerate >= distanceToDestination(newPosition)) {
				return MoveState.DECELERATING;
			} else {
				return MoveState.CRUISING;
			}
			break;

		case MoveState.DECELERATING: 
			Vector2 positionAfterDeceleration = Vector2.MoveTowards(Coordinates, destination.Coordinates, velocity.magnitude);
			if (velocity.magnitude - MaxAccel <= 0 && positionAfterDeceleration == destination.Coordinates) {
				return MoveState.STOPPED;
			} else {
				return MoveState.DECELERATING;
			}
			break;

		case MoveState.STOPPED:
			if (Coordinates == destination.Coordinates) {
				return MoveState.STOPPED;
			} else {
				return MoveState.ACCELERATING;
			}
			break;

		case MoveState.ACCELERATING:
			double accX = Destination.Coordinates.x - Coordinates.x;
			double accY = Destination.Coordinates.y - Coordinates.y;
			double n = MaxAccel / Math.Sqrt(Math.Pow(accX, 2) + Math.Pow(accY, 2)); 
			Vector2 acc = new Vector2 (Convert.ToSingle(accX * n), Convert.ToSingle(accY * n));
			Vector2 positionAfterAcceleration = velocity + acc;
			double finalVelocity = (velocity + acc).magnitude;

			double timeToDestination = distanceToDestination(positionAfterAcceleration) / finalVelocity;
			double timeToDecelerate2 = finalVelocity / MaxAccel;
			if (timeToDecelerate2 > timeToDestination) {
				return MoveState.DECELERATING;
			} else {
				if (finalVelocity >= MaxSpeed) {
					return MoveState.CRUISING;
				} else {
					return MoveState.ACCELERATING;
				}
			}
			break;
		}
		return currentMoveState; // To satisfy the compiler, the current state is returned if everything else fails.
	}

	private MoveState currentMoveState = MoveState.STOPPED;

	private Vector2 calculateAcceleration(double turnTime, bool accelerating) {
		double accX = Destination.Coordinates.x - Coordinates.x;
		double accY = Destination.Coordinates.y - Coordinates.y;
		double n = MaxAccel / Math.Sqrt(Math.Pow(accX, 2) + Math.Pow(accY, 2)); 
		Vector2 acc = new Vector2 (Convert.ToSingle(accX * n), Convert.ToSingle(accY * n));
		if (!accelerating) {
			return acc * -1;
		} else {
			return acc;
		}
	}

	public void Turn (double turnTime, double targetDate) {
		UnityEngine.Debug.Log("Location of Ship: " + Location);

		Vector2 acc = new Vector2(0, 0);

		currentMoveState = nextState();
		switch (currentMoveState) {
		case MoveState.STOPPED:
			velocity = acc;
			break;

		case MoveState.CRUISING:

			break;

		case MoveState.DECELERATING:
			acc = calculateAcceleration(turnTime, false);
			break;

		case MoveState.ACCELERATING: 
			acc = calculateAcceleration(turnTime, true);
			break;

		}
		velocity += acc * 0.5f;
		Vector2 distanceCovered = new Vector2 (velocity.x * Convert.ToSingle(turnTime), velocity.y * Convert.ToSingle(turnTime));
		localCoordinates += distanceCovered;
		velocity += acc * 0.5f;
	}

//		UnityEngine.Debug.Log("Location of Ship: " + Location);
//		float years = (float) (turnTime.TotalDays / 365);
////		if (Destination != null && Coordinates != Destination.Coordinates) {
////			Vector2 movement = Vector2.MoveTowards(Coordinates, Destination.Coordinates, MaxSpeed * years); // FIXME: This is wrong
////			UnityEngine.Debug.Log("Movement: " + movement);
////			localCoordinates = movement - location.LocalCoordinates;
////		}
//
//		float yearsLeftInTurn = years;
//
//		if (previousDestination != null) {
//			while (speed > 0.0f && yearsLeftInTurn > 0.0f && previousDestination != destination) {
//				moveTowards (previousDestination.Coordinates, speed * (yearsLeftInTurn > 1.0f ? 1.0f : yearsLeftInTurn));
//				decelerate();
//				UnityEngine.Debug.Log ("Current speed: " + speed);
//				yearsLeftInTurn = yearsLeftInTurn > 1.0f ? yearsLeftInTurn - 1.0f : 0.0f;
//			}
//		}
//
//		if (Destination != null && Coordinates != Destination.Coordinates) {
//			moveTowards (Destination.Coordinates, MaxSpeed * years);
//		}

//        UnityEngine.Debug.Log("Location of Ship: " + Location);
//		float years = (float) (turnTime.TotalDays / 365);
//
//		float yearsLeftInTurn = years;

		// Decelerate for correction
//		if (previousDestination != null) {
//			while (speed > 0.0f && yearsLeftInTurn > 0.0f && previousDestination != destination) {
//				moveTowards (previousDestination.Coordinates, speed * (yearsLeftInTurn > 1.0f ? 1.0f : yearsLeftInTurn));
//				decelerate();
//				yearsLeftInTurn = yearsLeftInTurn > 1.0f ? yearsLeftInTurn - 1.0f : 0.0f;
//			}
//		}

//		if (previousDestination != null && speed > 0.0f) {
//			if (years >= yearsRequiredToReachSpeed(0.0f)) {
//				float distance = distanceTravelled(speed, 0.0f); // (speed - 0.0) / 2
//				moveTowards(previousDestination.Coordinates, distance);
//				speed = 0.0f;
//				yearsLeftInTurn = yearsLeftInTurn - yearsRequiredToReachSpeed(0.0f);
//			} else { // years < yearsRequiredToReachSpeed(0.0f)
//				float maxDeceleration = years * MaxAccel;
//				float speedAtEndOfTurn = speed - maxDeceleration;
//				float distance = distanceTravelled(speed, speedAtEndOfTurn);
//				moveTowards(previousDestination.Coordinates, distance);
//				speed = speed - maxDeceleration;
//				yearsLeftInTurn = 0.0f;
//			}
//
//		}
//		//System.Diagnostics.Debug.Assert(speed < float.Epsilon || yearsLeftInTurn < float.Epsilon);
//
//		if (speed < float.Epsilon) {
//			previousDestination = destination;
//		}
//
//		if (yearsLeftInTurn > 0.0f) {
//	        if (Destination != null && Coordinates != Destination.Coordinates) {
//				// Accelerate
//				if (speed < MaxSpeed                                   // Not yet at full speed
//				    && Coordinates != Destination.Coordinates)         // Haven't yet reached destination 
//				{
//					bool canReachMaxSpeedInThisTurn = yearsLeftInTurn >= yearsRequiredToReachSpeed(MaxSpeed);
//					if (canReachMaxSpeedInThisTurn) {
//						float distance = distanceTravelled(speed, MaxSpeed);
//						Vector2 simulatedDestination = simulateMove(Destination.Coordinates, distance);
//						moveTowards(Destination.Coordinates, distance);
//						speed = MaxSpeed;
//						yearsLeftInTurn = yearsLeftInTurn - yearsRequiredToReachSpeed(MaxSpeed);
//					} else { // yearsLeftInTurn < yearsRequiredToReachSpeed(MaxSpeed);
//						float acceleration = yearsLeftInTurn * MaxAccel;
//						float distance = distanceTravelled(speed, (speed + acceleration));
//						moveTowards(Destination.Coordinates, distance);
//						speed = speed + acceleration;
//						yearsLeftInTurn = 0.0f;
//					}
//				}
//
//				// Cruise
//				while (yearsLeftInTurn > yearsRequiredToReachSpeed(0.0f)) {
//					float remainingDistance = Vector2.Distance(Coordinates, Destination.Coordinates);
//					float yearsToCruise = (remainingDistance - yearsRequiredToReachSpeed(0.0f)) / MaxSpeed;
//					if (yearsToCruise > 0 && yearsToCruise < yearsLeftInTurn) {
//						moveTowards(destination.Coordinates, speed * yearsToCruise);
//						yearsLeftInTurn -= yearsToCruise;
//					} else if (yearsToCruise > 0 && yearsToCruise > yearsLeftInTurn) {
//						// Cruise for the rest of the turn
//						moveTowards(destination.Coordinates, speed * yearsLeftInTurn);
//						yearsLeftInTurn -= yearsLeftInTurn;
//					}
//				}
//
//				// Decelerate to reach destination in an orderly fashion
//				while (speed > 0.0f && yearsLeftInTurn > 0.0f && Coordinates != Destination.Coordinates) {
//					decelerate();
//					moveTowards (previousDestination.Coordinates, speed * (yearsLeftInTurn > 1.0f ? 1.0f : yearsLeftInTurn));
//					yearsLeftInTurn = yearsLeftInTurn > 1.0f ? yearsLeftInTurn - 1.0f : 0.0f;
//				}
//
//
//	            //Vector2 movement = Vector2.MoveTowards(Coordinates, Destination.Coordinates, MaxSpeed * years); // FIXME: This is wrong
//	            //Debug.Log(movement);
//	            //localCoordinates = movement - location.LocalCoordinates;
//	        }
//		}
//		Vector3 movement = Vector3.MoveTowards(transform.localPosition, Destination.transform.localPosition, Speed * years);
//
//		if (Destination != null && Destination.transform.localPosition != transform.localPosition) {
//			iTween.MoveTo (gameObject, movement, 1.0f);
//		}
//

    public double AreaOfInfluence {
        get {
            return 0.1;
        }
    }

}
