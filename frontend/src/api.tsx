import axios from "axios"
import type { CompanySearch } from "./company";

interface SearchResponse {
    data: CompanySearch[];
}
export const searchCompanies = async (query: string) => {
    try {
        const data = await axios.get<SearchResponse>(
            
        )
    }
}