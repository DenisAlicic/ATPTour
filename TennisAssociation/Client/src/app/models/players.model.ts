export interface PlayerModel {
  id: string;
  firstName: string;
  lastName: string;
  country: string;
  height: number;
  weight: number;
  birth: Date;
  currentRankingSingle: number;
  bestRankingSingle: number;
  currentRankingDouble: number;
  bestRankingDouble: number;
  sex: string;
  hand: string;
  img: Int8Array;
}
