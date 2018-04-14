import {AppUserAuth} from "./app-user-auth";

export const LOGIN_MOCKS: AppUserAuth[] = [
    {
        userName: "jmosser",
        bearerToken: "abcdefghi89",
        isAuthenticated: true,
        canAccessProducts: true,
        canAddProduct: true,
        canSaveProduct: true,
        canAccessCategories: true,
        canAddCategory: false
    },
    {
        userName: "chmoxer",
        bearerToken: "abcdefghi99",
        isAuthenticated: true,
        canAccessProducts: false,
        canAddProduct: false,
        canSaveProduct: false,
        canAccessCategories: true,
        canAddCategory: true
    }
];