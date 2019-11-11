
import { VehicleDataRecords } from "src/data/structure/VehicleDataRecords";
import { WeighingImages } from "src/data/structure/WeghingImages";

export interface WeighingLog {
    id: number,
    vehicleId: number,
    vehiclePlate: string,
    vehiclePlateStencilId: number,
    trailerId: number,
    trailerPlate: string
    trailerPlateStencilId: number,
    timeStamp: string,
    scalesId: string
    operator: string,
    weight: number,
    previousWeighingId: number,
    type: number,
    flags: number,
    axlesWeighingFlags: number,
    isDeleted: boolean,
    deletedById: number,
    deletedOn: string,
    deletionReason: string,
    vehicle: VehicleDataRecords,
    previousWeighing: WeighingLog,
    weighingImages: WeighingImages[]
}
