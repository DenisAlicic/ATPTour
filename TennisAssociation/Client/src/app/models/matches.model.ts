export interface MatchModel {
  id: string;
  tournament: string;
  firstPlayer: string;
  secondPlayer: string;
  headToHeadFirst: number;
  headToHeadSecond: number;
  resultFirst: number;
  resultSecond: number;
  date: Date;
}
