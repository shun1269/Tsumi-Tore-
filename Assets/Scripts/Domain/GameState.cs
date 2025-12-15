public enum GameState
{
    Title,
    SelectMode,
    PracticePhase1_BuildTSD, // TSD構築フェーズ
    PracticePhase2_BuildPC,  // PC狙いフェーズ
    Paused,
    Result_Success,
    Result_Failed
}