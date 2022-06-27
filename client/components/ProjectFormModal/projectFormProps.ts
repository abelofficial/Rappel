import { CreateProjectRequestDto } from "../../types";
import { string, SchemaOf, object, boolean } from "yup";

export const fieldLabels: CreateProjectRequestDto = {
  title: "Title",
  description: "Description",
  isOrdered: false,
};

export const fieldNames: CreateProjectRequestDto = {
  title: "title",
  description: "description",
  isOrdered: false,
};

export const initialValues: CreateProjectRequestDto = {
  title: "",
  description: "",
  isOrdered: false,
};

export const validationSchema: SchemaOf<CreateProjectRequestDto> = object({
  title: string().required("Title is required"),
  isOrdered: boolean().required("Title is required"),
  description: string()
    .required("Description is required")
    .min(20, "Description must be min 20 characters."),
});
