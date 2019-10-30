export interface WeighingImages {
    id: number;
    type: string;
    cameraId: string;
    /* public Guid ImageKey { get; set; } */
    imageData: string;
    format: string;
    plateX: number;
    plateY: number;
    plateWidth: number;
    plateHeight: number;
    weighingId: number;
}
